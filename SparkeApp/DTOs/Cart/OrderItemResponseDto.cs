namespace SparkeApp.DTOs.Cart
{
    public class OrderItemResponseDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public int Subtotal { get; set; }
    }
}
