using System.ComponentModel.DataAnnotations;

namespace SparkeApp.DTOs.Category
{
    public class CreateCategoryDto
    {
        [Required]
        [StringLength(15, MinimumLength = 4)]
        public string Name { get; set; } = string.Empty;
       

    }
}
