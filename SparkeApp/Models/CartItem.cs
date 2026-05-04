using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SparkeApp.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        [ForeignKey("Cart")]
        public int CartId { get; set; } 
        public Cart Cart { get; set; } = default!; 
        public int ProductId { get; set; }
        public Product Product { get; set; } = default!;

    }
}
