using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SparkeApp.Models
{
    public class CartItem
    {
        // Fields 
        [Key]
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        // cart relationship 
        [ForeignKey("Cart")]
        public int CartId { get; set; } // FK to Cart
        public Cart Cart { get; set; } = default!; // one to many relationship with cart items

        // Product relationship 
        public int ProductId { get; set; } // FK to Product
        public Product Product { get; set; } = default!;

    }
}
