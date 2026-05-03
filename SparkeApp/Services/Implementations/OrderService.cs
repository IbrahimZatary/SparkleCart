using Microsoft.EntityFrameworkCore;
using SparkeApp.Data;
using SparkeApp.DTOs.Order;
using SparkeApp.Models;
using SparkeApp.Services.Interfaces;

namespace SparkeApp.Services.Implementations
{
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

        public Task<OrderDto> GetOrderByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OrderDto> UpdateOrderStatusAsync(UpdateOrderStatusDto updateDto)
        {
            throw new NotImplementedException();
        }
    }
}