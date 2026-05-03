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
            var products = await productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await productService.GetProductById(id);
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateUpdateProductDto createProductDto)
        {
            var product = await productService.CreateProductAsync(createProductDto);
            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] CreateUpdateProductDto updateProductDto)
        {
            var product = await productService.UpdateProductAsync(updateProductDto, id);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await productService.DeleteProductAsync(id);
            return Ok(result);

        }
    }
}