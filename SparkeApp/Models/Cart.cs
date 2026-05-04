using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SparkeApp.Models;

public class Cart
{
    public int Id { get; set; }
    [ForeignKey("User")]
    public int UserId { get; set; } 

    public User User { get; set; }  = default!;
    public ICollection<CartItem> CartItems { get; set; } = []; 
}
