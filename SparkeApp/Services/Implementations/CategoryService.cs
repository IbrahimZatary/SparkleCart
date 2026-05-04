using Microsoft.EntityFrameworkCore;
using SparkeApp.Data;
using SparkeApp.DTOs.Category;
using SparkeApp.Models;
using SparkeApp.Services.Interfaces;
namespace SparkeApp.Services.Implementations;

public class CategoryService(AppDbContext context) : ICategoryService
{

    public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto CreateCategory)
    {
        if(string.IsNullOrEmpty(CreateCategory.Name) )
            throw new ArgumentException("Category Name are required");
        

        var existingCategory = await context.Categories.FirstOrDefaultAsync(c => c.Name == CreateCategory.Name);
        if (existingCategory != null)
             throw new ArgumentException("Category with the same name already exists");
        

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
        var NewCategory =  await context.Categories.ToListAsync();

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
        return categoryDtos;
    }

    public async Task<DeleteCategoryResponseDto> DeleteCategoryAsync(int id)
    {
        var CurrentCategory = await context.Categories.FindAsync(id) ?? throw new KeyNotFoundException($"Category with ID {id} not found");
        context.Categories.Remove(CurrentCategory);

        await context.SaveChangesAsync();
        return new DeleteCategoryResponseDto($"Category with ID {id} deleted successfully");

    }
    
    public async Task<CategoryDto> GetCategoryAsync(int id)
    {
        var CurrentCategory= await context.Categories.FindAsync(id);
        return CurrentCategory is null
            ? throw new KeyNotFoundException($"Category with ID {id} not found")
            : new CategoryDto
        {
            Id = CurrentCategory.Id,
            Name = CurrentCategory.Name,
            Message = "Category retrieved successfully"
        };
    }

    public async Task<UpdateCreateResponseDto> UpdateCategoryAsync(UpdateCreateCategoryDto UpdateCategory)
    {
        var exsitingCategory = await context.Categories.FindAsync(UpdateCategory.Id) ?? throw new KeyNotFoundException($"Category with ID {UpdateCategory.Id} not found");
        string oldName = exsitingCategory.Name;
        exsitingCategory.Name = UpdateCategory.Name;

       await context.SaveChangesAsync();

        return new UpdateCreateResponseDto
        {
            Name = exsitingCategory.Name,
            Message = $"The Category  :  {oldName} ,  updated succesfuly into -  {exsitingCategory.Name} . "
        };  
    }
}
