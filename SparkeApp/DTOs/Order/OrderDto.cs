namespace SparkeApp.DTOs.Order;

public class OrderDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Status { get; set; } =default!;
    public int Price { get; set; }
    public List<OrderItemDto> Items { get; set; } = [];
}
