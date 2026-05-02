using SparkeApp.DTOs.Category;
using SparkeApp.Models;

namespace SparkeApp.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoryAsync();
        Task<CategoryDto> GetCategoryAsync(int id);
        Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto CreateCategory);

        Task<UpdateCreateResCat> UpdateCategoryAsync(UpdateCreateCategoryDto  UpdateCategory);
        Task <DeleteCategoryResponse> DeleteCategoryAsync(int id);
    }
}
