using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SparkeApp.DTOs.Product;
using SparkeApp.Services;
using SparkeApp.Services.Implementations;
using SparkeApp.Services.Interfaces;

namespace SparkeApp.Controllers;

[Route("api/product")]
[ApiController]
public class ProductController(IProductService productService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await productService.GetAllProductsAsync();
        return Ok(products);
    }
    // the paginated endpoint  
    [HttpGet("paginated")] 
    public async Task<IActionResult> GetProductsPaginated([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        if (pageSize > 100)
        {
            pageSize = 100; 
        }
        if (pageNumber < 1)
        {
            pageNumber = 1;
        }

        var result = await productService.GetProductsPaginatedAsync(pageNumber, pageSize);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var product = await productService.GetProductById(id);
        return Ok(product);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateProduct([FromBody] CreateUpdateProductDto createProductDto)
    {
        var product = await productService.CreateProductAsync(createProductDto);
        return Ok(product);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] CreateUpdateProductDto updateProductDto)
    {
        var product = await productService.UpdateProductAsync(updateProductDto, id);
        return Ok(product);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var result = await productService.DeleteProductAsync(id);
        return Ok(result);

    }
}