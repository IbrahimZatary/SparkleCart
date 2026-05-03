using SparkeApp.DTOs.Order;

namespace SparkeApp.Services.Interfaces
{
    public interface IOrderService
    {
        Task<ICollection<OrderDto>> GetAllOrdersAsync();
        Task<OrderDto> GetOrderByIdAsync(int id);
        Task<OrderDto> UpdateOrderStatusAsync(UpdateOrderStatusDto updateDto);
    }
}
