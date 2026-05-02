using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SparkeApp.Models;

    public class Product
    {
        public int Id { get; set; } // pk

    [Required]
        public string Name { get; set; } = default!;
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

        public string Description { get; set; } = default!;
    [Required]
        public int Quantity { get; set; }

    [ForeignKey("Category")] 
        public int CategoryId { get; set; } // FK
        // navigations props
        public Category Category { get; set; } = default!; // One product belongs to one category
        public ICollection<CartItem> CartItems { get; set; } = []; // One product may be in many cart items
        public ICollection<OrderItem> OrderItems { get; set; } = []; // One product may be in many order items

    }

