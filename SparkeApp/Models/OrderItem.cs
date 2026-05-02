using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SparkeApp.Models;

public class OrderItem
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("Order")]
    public int OrderId { get; set; } // FK to Order
    [ForeignKey("Product")]
    public int ProductId { get; set; } // FK to Product
    [Required]
    public int  Quantity { get; set; } 

   // navigations props  
  public Order Order { get; set; } = default!;
    public Product Product { get; set; } = default!;
}
