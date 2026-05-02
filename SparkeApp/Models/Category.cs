using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SparkeApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
        // navigations props
        public ICollection<Product> Products { get; set; } = [] ;
    }
}
