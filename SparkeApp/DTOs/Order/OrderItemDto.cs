namespace SparkeApp.DTOs.Order
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public int Subtotal { get; set; }
        public decimal UnitPrice { get; internal set; }
    }
}
