using Microsoft.EntityFrameworkCore;
using SparkeApp.Data;
using SparkeApp.DTOs.Category;
using SparkeApp.Models;
using SparkeApp.Services.Interfaces;
namespace SparkeApp.Services.Implementations
{
    public class CategoryService(AppDbContext context) : ICategoryService
    {

        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto CreateCategory)
        {
            // on creating name and type are required, so we will check if they are null or empty
            if(string.IsNullOrEmpty(CreateCategory.Name) )
            {
                throw new ArgumentException("Category Name are required");
            }


            // exsisting category with the same name should not be created not type because we can have multiple categories with the same type but not with the same name
            var existingCategory = await context.Categories.FirstOrDefaultAsync(c => c.Name == CreateCategory.Name);
            if (existingCategory != null)
            {
                throw new ArgumentException("Category with the same name already exists");
            }


            var NewCategory = new Category
            {
                Name = CreateCategory.Name,
            };


            await context.Categories.AddAsync(NewCategory);
            await context.SaveChangesAsync(); 


            return new CategoryDto
            {
                Id = NewCategory.Id,
                Name = NewCategory.Name,
                Message = "Category created successfully"

            };
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoryAsync()
        {
            //Get all categories from database
            var NewCategory =  await context.Categories.ToListAsync();

            // Convert Entity to DTO(manual mapping)
            var categoryDtos = new List<CategoryDto>();
            foreach (var category in NewCategory)
            {
                categoryDtos.Add(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    Message = "Category retrieved successfully"
                });
            }

            // Return DTOs
            return categoryDtos;


            // get from db > entity > dto response 
        }

        public async Task<DeleteCategoryResponse> DeleteCategoryAsync(int id)
        {
           var CurrentCategory= await context.Categories.FindAsync(id);
            if (CurrentCategory == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found");

            }
            context.Categories.Remove(CurrentCategory);

            await context.SaveChangesAsync();

            return new DeleteCategoryResponse
            {
                Message = $"Category with ID {id} deleted successfully"
            };
        }

      


        public async Task<CategoryDto> GetCategoryAsync(int id)
        {
            var CurrentCategory= await context.Categories.FindAsync(id);
            if (CurrentCategory == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found");

            }
            // convert it into dto 
            return new CategoryDto
            {
                Id = CurrentCategory.Id,
                Name = CurrentCategory.Name,
                Message = "Category retrieved successfully"
            };

         
        }

        public async Task<UpdateCreateResCat> UpdateCategoryAsync(UpdateCreateCategoryDto UpdateCategory)
        {
            // get the id   
            var exsitingCategory = await context.Categories.FindAsync(UpdateCategory.Id);
            // check if the category exist or not
            if (exsitingCategory == null)
                throw new KeyNotFoundException($"Category with ID {UpdateCategory.Id} not found");
            
            string oldName = exsitingCategory.Name;
            exsitingCategory.Name = UpdateCategory.Name;

             await context.SaveChangesAsync();


            return new UpdateCreateResCat
            {
                Name = exsitingCategory.Name,
                Message = $"The Category  :  {oldName} ,  updated succesfuly into -  {exsitingCategory.Name} . "
            };  
        }
    }

}
