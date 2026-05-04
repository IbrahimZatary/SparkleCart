namespace SparkeApp.DTOs.Cart;

public class OrderResponseDto
{
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public int TotalPrice { get; set; }  
    public string Status { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
    public string ShippingAddress { get; set; } = string.Empty;
    public List<OrderItemResponseDto> Items { get; set; } = [];
    public string Message { get; set; } = string.Empty;
}
