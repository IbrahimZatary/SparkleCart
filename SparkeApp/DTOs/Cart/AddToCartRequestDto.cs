using System.ComponentModel.DataAnnotations;

namespace SparkeApp.DTOs.Cart;

public class AddToCartRequestDto
{
    [Required]
    public int ProductId { get; set; }
    [Required]
    public int Quantity { get; set; }
}
