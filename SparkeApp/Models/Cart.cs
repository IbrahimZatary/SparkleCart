using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SparkeApp.Models;

public class Cart
{
    public int Id { get; set; }
    [ForeignKey("User")]
    public int UserId { get; set; } // FK to User
    // Navigation prop 
    public User User { get; set; }  = default!; // one to one relationship with user
    public ICollection<CartItem> CartItems { get; set; } = []; // one to many relationship with cart items
}
