using Microsoft.AspNetCore.Mvc;
using SparkeApp.DTOs.Product;
using SparkeApp.Services;
using SparkeApp.Services.Interfaces;

namespace SparkeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await productService.GetAllProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving products", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await productService.GetProductById(id);
                return Ok(product);
            }
            catch (Exception ex) when (ex.Message == "Product not found")
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving product", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateUpdateProductDto createProductDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var product = await productService.CreateProductAsync(createProductDto);
                return Ok(product);
            }
            catch (Exception ex) when (ex.Message.Contains("Category not found"))
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating product", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] CreateUpdateProductDto updateProductDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var product = await productService.UpdateProductAsync(updateProductDto, id);
                return Ok(product);
            }
            catch (Exception ex) when (ex.Message.Contains("not found"))
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex) when (ex.Message.Contains("Category not found"))
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating product", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var result = await productService.DeleteProductAsync(id);
                return Ok(result);
            }
            catch (Exception ex) when (ex.Message.Contains("not found"))
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting product", error = ex.Message });
            }
        }
    }
}