using Microsoft.EntityFrameworkCore;
using SparkeApp.Data;
using SparkeApp.DTOs.Product;
using SparkeApp.Models;
using SparkeApp.Exceptions;
using SparkeApp.Services.Interfaces;

namespace SparkeApp.Services.Implementations
{
    public class ProductService(AppDbContext context) : IProductService
    {
        public async Task<ProductDto> CreateProductAsync(CreateUpdateProductDto createProduct)
        {
            var category = await context.Categories.FindAsync(createProduct.CategoryId) ?? throw new NotFoundException("Category not found , You can't add product here ");
            var productRequest = new Product
            {
                Name = createProduct.Name,
                Price = createProduct.Price,
                Description = createProduct.Description,
                Quantity = createProduct.Quantity,
                CategoryId = createProduct.CategoryId
            };
            context.Products.Add(productRequest);
            await context.SaveChangesAsync();
            return new ProductDto
            { 
            Id = productRequest.Id,
             Name = productRequest.Name,
             Quantity = productRequest.Quantity,
             CategoryId = productRequest.CategoryId,
             CategoryName = category.Name ,
             Message = "Product created successfully"
            };
        }

        public async Task<DeleteProductDto> DeleteProductAsync(int id)
        {
            var existingProduct = await context.Products
                 .Include(p => p.CartItems)
                 .Include(p => p.OrderItems)
                 .FirstOrDefaultAsync(p => p.Id == id) ?? throw new NotFoundException("Product not found to deleted ");
          
            var cartItemsCount = existingProduct.CartItems?.Count ?? 0; 
            var orderItemsCount = existingProduct.OrderItems?.Count ?? 0;

            context.Products.Remove(existingProduct);
            await context.SaveChangesAsync();

            return new DeleteProductDto
            {
                Id = existingProduct.Id,
                CartItemsCount = cartItemsCount, 
                OrderItemsCount = orderItemsCount, 
                ConfirmationMessage = "Product deleted successfully"
            };

        }

        public async Task<IEnumerable<GetAllProductResponseDto>> GetAllProductsAsync()
        {
        var products = await context.Products
       .Include(p => p.Category)
       .Select(p => new GetAllProductResponseDto
       {
           Id = p.Id,
           Name = p.Name,
           Quantity = p.Quantity,
           CategoryName = p.Category != null ? p.Category.Name : "Unknown" ,
           CategoryId = p.CategoryId
       })
       .ToListAsync();
            return products;
        }

        public async Task<GetProductByIdResponseDto> GetProductById(int id)
        {
            var ExsistingProduct = await context.Products.FindAsync(id) ?? throw new NotFoundException("Product not found");

            var category = await context.Categories.FindAsync(ExsistingProduct.CategoryId);
            return category is null
                ? throw new NotFoundException("Category not found for specific Id")
                : new GetProductByIdResponseDto
            {
                Id = ExsistingProduct.Id,
                Name = ExsistingProduct.Name,
                Price = ExsistingProduct.Price,
                Description = ExsistingProduct.Description,
                Quantity = ExsistingProduct.Quantity,
                Message = "Product retrived"
            };
        }


        public async Task<UpdateProductResponse> UpdateProductAsync(CreateUpdateProductDto updateProduct , int id)
        {
            var existingProduct = await context.Products
                .Include(p => p.Category)
                .Include(p => p.CartItems)
                .Include(p => p.OrderItems)
                .FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new NotFoundException($"Product with ID {id} not found");

            var category = await context.Categories.FindAsync(updateProduct.CategoryId) ?? throw new NotFoundException($"Category with ID {updateProduct.CategoryId} not found");

            var oldName = existingProduct.Name;
            var oldPrice = existingProduct.Price;
            var oldCategoryId = existingProduct.CategoryId;

            existingProduct.Name = updateProduct.Name;
            existingProduct.Price = updateProduct.Price;
            existingProduct.Description = updateProduct.Description;
            existingProduct.Quantity = updateProduct.Quantity;
            existingProduct.CategoryId = updateProduct.CategoryId;

            context.Products.Update(existingProduct);
            await context.SaveChangesAsync();

            return new UpdateProductResponse
            {
                Id = existingProduct.Id,
                Name = existingProduct.Name,
                Price = existingProduct.Price,
                Description = existingProduct.Description,
                Quantity = existingProduct.Quantity,
                Message = "Product updated successfully"
            };
        }

    }
} 


