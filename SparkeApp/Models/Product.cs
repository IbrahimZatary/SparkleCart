using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SparkeApp.Models;

    public class Product
    {
        public int Id { get; set; } 
    [Required]
        public string Name { get; set; } = default!;
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
        public string Description { get; set; } = default!;
    [Required]
        public int Quantity { get; set; }

    [ForeignKey("Category")] 
        public int CategoryId { get; set; } 
        public Category Category { get; set; } = default!; 
        public ICollection<CartItem> CartItems { get; set; } = []; 
        public ICollection<OrderItem> OrderItems { get; set; } = []; 

    }

