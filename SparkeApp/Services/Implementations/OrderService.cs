using Microsoft.EntityFrameworkCore;
using SparkeApp.Data;
using SparkeApp.DTOs.Order;
using SparkeApp.Exceptions;
using SparkeApp.Models;
using SparkeApp.Enums;
using SparkeApp.Services.Interfaces;

namespace SparkeApp.Services.Implementations;

public class OrderService(AppDbContext context) : IOrderService
{

    public async Task<ICollection<OrderDto>> GetAllOrdersAsync()
    {
        var orders = await context.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .Include(o => o.User)
            .OrderByDescending(o => o.Id)
            .ToListAsync();

        if (orders is null || orders.Count == 0)
        {
            return [];
        }

        return [.. orders.Select(o => new OrderDto
        {
            Id = o.Id,
            UserId = o.UserId,
            UserName = o.User?.Name ?? "Unknown User",
            Status =  o.Status.ToString(),
            Price = o.Price,
            Items = [.. o.OrderItems.Select(oi => new OrderItemDto
            {
                Id = oi.Id,
                ProductId = oi.ProductId,
                ProductName = oi.Product?.Name ?? "Unknown Product",
                Quantity = oi.Quantity,
            })]
        })];
    }

    public async Task<OrderDto> GetOrderByIdAsync(int id)
    {
        var order = await context.Orders.Include(o => o.OrderItems)
      .ThenInclude(oi => oi.Product)
    .Include(o => o.User)
    .FirstOrDefaultAsync(o => o.Id == id);

        return order is null
            ? throw new NotFoundException($"Order with ID {id} not found")
            : new OrderDto
        {
            Id = order.Id,
            UserId = order.UserId,
            UserName = order.User?.Name ?? "Unknown User",
            Status = order.Status.ToString(),
                Price = order.Price,
            Items = [.. order.OrderItems.Select(oi => new OrderItemDto
            {
                Id = oi.Id,
                ProductId = oi.ProductId,
                ProductName = oi.Product?.Name ?? "Unknown Product",
                Quantity = oi.Quantity,
                UnitPrice = oi.Product.Price,
                Subtotal = (int)(oi.Quantity * oi.Product.Price)
            })]
            };
    }

    public async Task<OrderDto> UpdateOrderStatusAsync(UpdateOrderStatusDto updateDto)
    {
        var order = await context.Orders
      .Include(o => o.OrderItems)
      .ThenInclude(oi => oi.Product)
      .Include(o => o.User)
     .FirstOrDefaultAsync(o => o.Id == updateDto.OrderId) ?? throw new NotFoundException($"Order with ID {updateDto.OrderId} not found");

        order.Status = updateDto.Status;
        await context.SaveChangesAsync();
        return new OrderDto
        {
            Id = order.Id,
            UserId = order.UserId,
            UserName = order.User?.Name ?? "Unknown User",
            Status = order.Status.ToString(),
            Price = order.Price,
            Items = [.. order.OrderItems.Select(oi => new OrderItemDto
            {
                Id = oi.Id,
                ProductId = oi.ProductId,
                ProductName = oi.Product?.Name ?? "Unknown Product",
                Quantity = oi.Quantity,
                UnitPrice = oi.Product.Price,
                Subtotal = (int)(oi.Quantity * oi.Product.Price)
            })]
        };

    }
}

