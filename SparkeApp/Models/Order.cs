using SparkeApp.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SparkeApp.Models;

public class Order
{
    public int Id { get; set; }  
    [ForeignKey("User")]
    public int UserId { get; set; } 
    [Required]
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    [Required]
    public int Price { get; set; }
    public User User { get; set; } = default!;
    public ICollection<OrderItem> OrderItems { get; set; } = [];
}
