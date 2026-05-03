using Microsoft.EntityFrameworkCore;
using SparkeApp.Data;
using SparkeApp.DTOs.Product;
using SparkeApp.Models;
using SparkeApp.Services.Interfaces;

namespace SparkeApp.Services.Implementations
{
    public class ProductService(AppDbContext context) : IProductService
    {
        public async Task<ProductDto> CreateProductAsync(CreateUpdateProductDto createProduct)
        {
            // check if category exists
            var category = await context.Categories.FindAsync(createProduct.CategoryId);
            if (category == null) 
                throw new Exception("Category not found , You can't add product here ");
            //  assing the values to the product model
            var productRequest = new Product
            {
                Name = createProduct.Name,
                Price = createProduct.Price,
                Description = createProduct.Description,
                Quantity = createProduct.Quantity,
                CategoryId = createProduct.CategoryId
            };
            // add the product to the database
            context.Products.Add(productRequest);
            await context.SaveChangesAsync();
            // return the product response
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
            // include the product with its related cart items and order items to get the counts before deletion
            var existingProduct = await context.Products
                 .Include(p => p.CartItems)
                 .Include(p => p.OrderItems)
                 .FirstOrDefaultAsync(p => p.Id == id) ?? throw new Exception("Product not found to deleted ");
          

            var cartItemsCount = existingProduct.CartItems?.Count ?? 0; 
            var orderItemsCount = existingProduct.OrderItems?.Count ?? 0;

            context.Products.Remove(existingProduct);
            await context.SaveChangesAsync();

            return new DeleteProductDto
            {
                Id = existingProduct.Id,
                CartItemsCount = cartItemsCount, //  Tells user how many carts affected
                OrderItemsCount = orderItemsCount, // Tells user how many orders affected
                ConfirmationMessage = "Product deleted successfully"
            };

        }

        public async Task<IEnumerable<GetAllProductResponseDto>> GetAllProductsAsync()
        {
            // get all products with their categories
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
            // return the list of products
            return products;

        }

        public async Task<GetProductByIdResponseDto> GetProductById(int id)
        {
            // check if the product exists
            var ExsistingProduct = await context.Products.FindAsync(id);
            if (ExsistingProduct == null)
                throw new Exception("Product not found");
            // get the category of the product
            var category = await context.Categories.FindAsync(ExsistingProduct.CategoryId);
          if (category == null)
            throw new Exception("Category not found for specific Id");
          return  new GetProductByIdResponseDto
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
            // Find the existing product with related data
            var existingProduct = await context.Products
                .Include(p => p.Category)
                .Include(p => p.CartItems)
                .Include(p => p.OrderItems)
                .FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new Exception($"Product with ID {id} not found");

            // if the user want to change the category of the product we need to check if the new category exists
            var category = await context.Categories.FindAsync(updateProduct.CategoryId);
            if (category == null)
                throw new Exception($"Category with ID {updateProduct.CategoryId} not found");

            //  Store old values for logging/response to use it as variables in dto 
            var oldName = existingProduct.Name;
            var oldPrice = existingProduct.Price;
            var oldCategoryId = existingProduct.CategoryId;

            //  Update product properties
            existingProduct.Name = updateProduct.Name;
            existingProduct.Price = updateProduct.Price;
            existingProduct.Description = updateProduct.Description;
            existingProduct.Quantity = updateProduct.Quantity;
            existingProduct.CategoryId = updateProduct.CategoryId;
            //  Save changes to database
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


