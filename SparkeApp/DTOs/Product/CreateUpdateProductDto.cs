using System.ComponentModel.DataAnnotations;

namespace SparkeApp.DTOs.Product
{
    public class CreateUpdateProductDto
    {
        [Required]
        public string Name { get; set; } = default!;

        [Required]
        //[Range(0, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        public string Description { get; set; } = default!;

        [Required]
        //[Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative")]
        public int Quantity { get; set; }

        [Required]
        public int CategoryId { get; set; } // Must specify which category
    }
}
