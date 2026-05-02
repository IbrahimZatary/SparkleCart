using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SparkeApp.Models;

public class Order
{
    public int Id { get; set; } // PK 
    [ForeignKey("User")]
    public int UserId { get; set; } // FK to User related
    [Required]
    public string Status { get; set; }= default!; // e.g., "Pending", "Shipped", "Delivered"
    [Required]
    public int Price { get; set; }
    // navigations props
    public User User { get; set; } = default!;
    public ICollection<OrderItem> OrderItems { get; set; } = [];
}
