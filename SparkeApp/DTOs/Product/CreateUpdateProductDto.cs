using System.ComponentModel.DataAnnotations;

namespace SparkeApp.DTOs.Product;

public class CreateUpdateProductDto
{
    [Required]
    public string Name { get; set; } = default!;
    [Required]
    public decimal Price { get; set; }
    public string Description { get; set; } = default!;
    [Required]
    public int Quantity { get; set; }
    [Required]
    public int CategoryId { get; set; } 
}
