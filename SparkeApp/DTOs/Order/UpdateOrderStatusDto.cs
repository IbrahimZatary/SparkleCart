using SparkeApp.Enums;
using System.ComponentModel.DataAnnotations;

namespace SparkeApp.DTOs.Order;
public class UpdateOrderStatusDto
{
    [Required]
    public int OrderId { get; set; }

    [Required]
    public OrderStatus Status { get; set; }
}
