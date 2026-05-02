namespace SparkeApp.DTOs.Cart
{
    public class CartResponseDto
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int TotalPrice { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
