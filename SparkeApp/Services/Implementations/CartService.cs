using Azure.Core;
using Microsoft.EntityFrameworkCore;
using SparkeApp.Data;
using SparkeApp.DTOs.Cart;
using SparkeApp.Models;
using SparkeApp.Enums;
using SparkeApp.Services.Interfaces;
using SparkeApp.Exceptions;
using System.Net;

namespace SparkeApp.Services.Implementations
{
    public class CartService(AppDbContext context) : ICartService
    {
        public async Task<CartResponseDto> AddToCartAsync(int userId, AddToCartRequestDto requestDto)
        {

            var cart = await context.Carts.FirstOrDefaultAsync(c => c.UserId == userId) ?? throw new NotFoundException("No cart found for this user. Please register first.");
            var product = await context.Products.FindAsync(requestDto.ProductId) ?? throw new NotFoundException("Product not found");

            if (product.Quantity < requestDto.Quantity)
            {
                throw new ConflictException(
                    $"Only {product.Quantity} items available in stock. You requested {requestDto.Quantity}.");
            }
            var cartItem = new CartItem
            {
                CartId = cart.Id,
                ProductId = requestDto.ProductId,
                Quantity = requestDto.Quantity
            };
            context.CartItems.Add(cartItem);
            await context.SaveChangesAsync();

            return new CartResponseDto
            {
                CartId = cart.Id,
                UserId = userId,
                Message = "Product added to cart successfully"
            };

        }

        public async Task<CartResponseForUserDto> GetCartByUser(int userId)
        {

            var user = await context.Users.FindAsync(userId);

            if (user is null)
            {
                return new CartResponseForUserDto
                {
                    UserId = userId,
                    TotalPrice = 0,
                    Message = "User not found"
                };
            }

            var cart = await context.Carts
          .Include(c => c.CartItems)
           .ThenInclude(ci => ci.Product)
           .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart.CartItems is null || cart.CartItems.Count == 0)
            {
                return new CartResponseForUserDto
                {
                    CartId = cart.Id,
                    UserId = userId,
                    TotalPrice = 0,
                    Message = "Cart is empty"
                };
            }
            var items = cart.CartItems.Select(ci => new CartItemResponseDto
            {
                ProductId = ci.ProductId,
                ProductName = ci.Product.Name,
                ProductPrice = ci.Product.Price,
                Quantity = ci.Quantity,
                Subtotal = ci.Quantity * ci.Product.Price
            }).ToList();

            decimal totalPrice = items.Sum(i => i.Subtotal);

            return new CartResponseForUserDto
            {
                CartId = cart.Id,
                UserId = userId,
                TotalPrice = totalPrice,
                Items = items,  
                Message = "Cart retrieved successfully"
            };
        
        }

        public async Task<UpdateQResponseDto> UpdateQuantityAsync(UpdateQDto updateQDto)
        {
            var cartItem = await context.CartItems
           .Include(ci => ci.Product)
           .FirstOrDefaultAsync(ci => ci.Id == updateQDto.CartItemId) ?? throw new NotFoundException("Cart item not found");

            if (updateQDto.QuantityRequired > cartItem.Product.Quantity)
                throw new ConflictException($"Only {cartItem.Product.Quantity} items available , check later for more .");
            cartItem.Quantity = updateQDto.QuantityRequired;

            await context.SaveChangesAsync();

            return new UpdateQResponseDto
            {
                CartItemId = cartItem.Id,
                NewQuantity = updateQDto.QuantityRequired,
                Message = "Quantity updated successfully"
            };
        }

        public async Task<OrderResponseDto> CheckoutAsync(CheckoutRequestDto request)
        {
            var cart = await context.Carts
               .Include(c => c.CartItems)
               .ThenInclude(ci => ci.Product)
               .FirstOrDefaultAsync(c => c.UserId == request.UserId);

            if (cart is null || cart.CartItems.Count == 0)
                throw new BadRequestException("Cart is empty.  You Cannot checkout. ");

            int totalPrice = (int)cart.CartItems.Sum(ci => ci.Quantity * ci.Product.Price);
            var order = new Order
            {
                UserId = request.UserId,    
                Status = OrderStatus.Pending,
                Price = totalPrice
            };

            context.Orders.Add(order);
            await context.SaveChangesAsync();

            var orderItems = new List<OrderItem>();

            foreach (var cartItem in cart.CartItems)
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.Id,                    
                    ProductId = cartItem.ProductId,       
                    Quantity = cartItem.Quantity,          
                };
                orderItems.Add(orderItem);  

                var product = cartItem.Product;
                product.Quantity -= cartItem.Quantity; context.Products.Update(product);
            }

            context.OrderItems.AddRange(orderItems);
            context.CartItems.RemoveRange(cart.CartItems);
            await context.SaveChangesAsync();

            return new OrderResponseDto
            {
                OrderId = order.Id,
                UserId = request.UserId,
                TotalPrice = totalPrice,
                Status = order.Status.ToString(),
                PaymentMethod = request.PaymentMethod,
                ShippingAddress = request.ShippingAddress,
                Items = [.. orderItems.Select(oi => new OrderItemResponseDto
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product.Name,   
                    Quantity = oi.Quantity,
                    Subtotal = (int)(oi.Quantity * oi.Product.Price)
                })],
                Message = "Order placed successfully! and  Cart has been cleared."
            };
        }

    }
}
