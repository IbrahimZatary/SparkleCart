using System.ComponentModel.DataAnnotations;

namespace SparkeApp.DTOs.Order
{
    public class UpdateOrderStatusDto
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public string Status { get; set; } = string.Empty;
    }
}
