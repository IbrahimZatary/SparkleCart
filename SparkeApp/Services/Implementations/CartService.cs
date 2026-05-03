using Azure.Core;
using Microsoft.EntityFrameworkCore;
using SparkeApp.Data;
using SparkeApp.DTOs.Cart;
using SparkeApp.Models;
using SparkeApp.Services.Interfaces;
using System.Net;

namespace SparkeApp.Services.Implementations
{
    public class CartService(AppDbContext context) : ICartService
    {
        public async Task<CartResponseDto> AddToCartAsync(int userId, AddToCartRequestDto requestDto)
        {

            // check if the cart id entered is available 
            var cart = await context.Carts.FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart == null)
                throw new InvalidOperationException("No cart found for this user. Please register first.");

            //check if product exisit 
            var product = await context.Products.FindAsync(requestDto.ProductId);
            if (product == null)
            {
                throw new ArgumentException("Product not found");
            }
            // quantity check 
            if (product.Quantity < requestDto.Quantity)
            {
                throw new InvalidOperationException(
                    $"Only {product.Quantity} items available in stock. You requested {requestDto.Quantity}.");
            }
            // add prop into cart items // add product into cartItems
            var cartItem = new CartItem
            {
                CartId = cart.Id,
                ProductId = requestDto.ProductId,
                Quantity = requestDto.Quantity
            };
            // save changes 
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

            // Check the id &cart  is exist
            //  Check if user exists
            var user = await context.Users.FindAsync(userId);

            if (user == null)
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

            //  Check if cart has items
            if (cart.CartItems == null || !cart.CartItems.Any())
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

            //Calculate total price
            decimal totalPrice = items.Sum(i => i.Subtotal);

            return new CartResponseForUserDto
            {
                CartId = cart.Id,
                UserId = userId,
                TotalPrice = totalPrice,
                Items = items,  // ← All products in cart
                Message = "Cart retrieved successfully"
            };

            // get the cart for specefic user
            // list all products in the cart as list 
        }

        public async Task<UpdateQResponseDto> UpdateQuantityAsync(UpdateQDto updateQDto)
        {
            // check cartItems like you do an update for cartItem and its not available 
            var cartItem = await context.CartItems
           .Include(ci => ci.Product)
           .FirstOrDefaultAsync(ci => ci.Id == updateQDto.CartItemId);
            if (cartItem == null)
                throw new ArgumentException("Cart item not found");


            // quantity check 
            if (updateQDto.QuantityRequired > cartItem.Product.Quantity)
                throw new InvalidOperationException($"Only {cartItem.Product.Quantity} items available , check later for more .");
            //update quantity 
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
            //Method that takes checkout info(userId, payment method, address) and returns order details.
            var cart = await context.Carts
               .Include(c => c.CartItems)
               .ThenInclude(ci => ci.Product)
               .FirstOrDefaultAsync(c => c.UserId == request.UserId);

            //check if cart is empty 
            if (cart == null || !cart.CartItems.Any())
                throw new InvalidOperationException("Cart is empty.  You Cannot checkout. ");



            int totalPrice = (int)cart.CartItems.Sum(ci => ci.Quantity * ci.Product.Price);
            var order = new Order
            {
                UserId = request.UserId,     // Which user owns this order
                Status = "Pending",           
                Price = totalPrice
            };

            context.Orders.Add(order);
            await context.SaveChangesAsync();



            // Prepare list to hold order items
            var orderItems = new List<OrderItem>();


            foreach (var cartItem in cart.CartItems)
            {
                // Convert CartItem to OrderItem
                var orderItem = new OrderItem
                {
                    OrderId = order.Id,                    // Link to new order
                    ProductId = cartItem.ProductId,        // Which product
                    Quantity = cartItem.Quantity,          // How many
                };
                orderItems.Add(orderItem);  // Add to list 

                // update  product stock (reduce available quantity)
                var product = cartItem.Product;
                product.Quantity -= cartItem.Quantity; context.Products.Update(product);
            }



            context.OrderItems.AddRange(orderItems);
            // remove all cartItems
            context.CartItems.RemoveRange(cart.CartItems);
            await context.SaveChangesAsync();


            return new OrderResponseDto
            {
                OrderId = order.Id,
                UserId = request.UserId,
                TotalPrice = totalPrice,
                Status = order.Status,
                PaymentMethod = request.PaymentMethod,
                ShippingAddress = request.ShippingAddress,
                Items = orderItems.Select(oi => new OrderItemResponseDto
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product.Name,     // Get product name from database
                    Quantity = oi.Quantity,
                    Subtotal = (int)(oi.Quantity * oi.Product.Price)
                }).ToList(),
                Message = "Order placed successfully! and  Cart has been cleared."
            };


        }


    }
}
