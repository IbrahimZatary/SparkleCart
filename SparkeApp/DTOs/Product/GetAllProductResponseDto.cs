namespace SparkeApp.DTOs.Product;

public class GetAllProductResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public int Quantity { get; set; }
    public int CategoryId { get; set; }
    public decimal Price { get; set; }
    public string CategoryName { get; set; } = default!;
}
