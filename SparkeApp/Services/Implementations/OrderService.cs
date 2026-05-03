using Microsoft.EntityFrameworkCore;
using SparkeApp.Data;
using SparkeApp.DTOs.Order;
using SparkeApp.Models;
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

        if (orders == null || !orders.Any())
        {
            return new List<OrderDto>();
        }

        return orders.Select(o => new OrderDto
        {
            Id = o.Id,
            UserId = o.UserId,
            UserName = o.User?.Name ?? "Unknown User",
            Status = o.Status,
            Price = o.Price,
            Items = o.OrderItems.Select(oi => new OrderItemDto
            {
                Id = oi.Id,
                ProductId = oi.ProductId,
                ProductName = oi.Product?.Name ?? "Unknown Product",
                Quantity = oi.Quantity,
            }).ToList()
        }).ToList();
    }

    public async Task<OrderDto> GetOrderByIdAsync(int id)
    {
        var order = await context.Orders.Include(o => o.OrderItems)
      .ThenInclude(oi => oi.Product)
    .Include(o => o.User)
    .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null)
        {
            throw new KeyNotFoundException($"Order with ID {id} not found");
        }
        return new OrderDto
        {
            Id = order.Id,
            UserId = order.UserId,
            UserName = order.User?.Name ?? "Unknown User",
            Status = order.Status,
            Price = order.Price,
            Items = order.OrderItems.Select(oi => new OrderItemDto
            {
                Id = oi.Id,
                ProductId = oi.ProductId,
                ProductName = oi.Product?.Name ?? "Unknown Product",
                Quantity = oi.Quantity,
                UnitPrice = oi.Product.Price,
                Subtotal = (int)(oi.Quantity * oi.Product.Price)
            }).ToList()
        };



    }

    public async Task<OrderDto> UpdateOrderStatusAsync(UpdateOrderStatusDto updateDto)
    {
           var order = await context.Orders
         .Include(o => o.OrderItems)
         .ThenInclude(oi => oi.Product)
         .Include(o => o.User)
        .FirstOrDefaultAsync(o => o.Id == updateDto.OrderId);

        if (order == null)
        {
            throw new KeyNotFoundException($"Order with ID {updateDto.OrderId} not found");
        }
        // status restrict to be like must 
        var validStatuses = new[] { "Pending", "Paid", "Shipped", "Delivered", "Cancelled" };
        if (!validStatuses.Contains(updateDto.Status))
        {
            throw new ArgumentException($"Invalid status. Allowed values: {string.Join(", ", validStatuses)}");
        } 

        throw new NotImplementedException();   

    }
}