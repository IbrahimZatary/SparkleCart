namespace SparkeApp.DTOs.Cart
{
    public class CartItemResponseDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; internal set; } // Quantity * Price
    }
}
