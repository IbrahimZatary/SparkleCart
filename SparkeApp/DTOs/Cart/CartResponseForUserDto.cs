namespace SparkeApp.DTOs.Cart
{
    public class CartResponseForUserDto
    {
        public int UserId { get; internal set; }
        public string Message { get; internal set; } = default!;
        public decimal TotalPrice { get; internal set; }
        public object CartId { get; internal set; } = default!;
        public List<CartItemResponseDto> Items { get; internal set; } // to list all the products 
    }
}
