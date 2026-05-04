using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using SparkeApp.Data;
using SparkeApp.DTOs.Category;
using SparkeApp.Services.Implementations;
using SparkeApp.Services.Interfaces;
using System.Diagnostics;

namespace SparkeApp.Controllers;

[Route("api/category")]
[ApiController]
public class CategoryController(ICategoryService CategoryService) : ControllerBase
{

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto createCategory)
    {
        var result = await CategoryService.CreateCategoryAsync(createCategory);
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var result = await CategoryService.GetAllCategoryAsync();
        return Ok(result);
    }
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCreateCategoryDto updateCategory)
    {

        var result = await CategoryService.UpdateCategoryAsync(updateCategory);
        return Ok(result);

    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var result = await CategoryService.DeleteCategoryAsync(id);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategory(int id)
    {
        var result = await CategoryService.GetCategoryAsync(id);
        return Ok(result);
    }
}
