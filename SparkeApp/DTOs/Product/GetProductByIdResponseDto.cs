namespace SparkeApp.DTOs.Product;

public class GetProductByIdResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
    public string Description { get; set; } = default!;
    public int Quantity { get; set; }
   public string Message { get; set; } = default!;
}
