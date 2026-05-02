using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using SparkeApp.Data;
using SparkeApp.DTOs.Category;
using SparkeApp.Services.Implementations;
using SparkeApp.Services.Interfaces;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SparkeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryService CategoryService) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto createCategory)
        {
            try
            {
                var result = await CategoryService.CreateCategoryAsync(createCategory);
                return Ok(result);

            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var result = await CategoryService.GetAllCategoryAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCreateCategoryDto updateCategory)
        {
            try
            {
                if (id != updateCategory.Id)
                {
                    return BadRequest(new { error = "ID in URL does not match ID in request body" });
                }
                var result = await CategoryService.UpdateCategoryAsync(updateCategory);
                return Ok(result);
            }

            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to update category the reason is ", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var result = await CategoryService.DeleteCategoryAsync(id);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to delete category the reason is ", details = ex.Message });
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            try
            {
                var result = await CategoryService.GetCategoryAsync(id);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to retrieve category the reason is ", details = ex.Message });
            }
        }
    }
}
