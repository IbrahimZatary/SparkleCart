using SparkeApp.DTOs;
using SparkeApp.DTOs.Product;
namespace SparkeApp.Services;

public interface IProductService
{
    Task<PaginatedResponseDto<GetAllProductResponseDto>> GetProductsPaginatedAsync(int pageNumber, int pageSize);
    Task<GetProductByIdResponseDto> GetProductById(int id);
    Task<ProductDto> CreateProductAsync(CreateUpdateProductDto createProduct);
    Task<UpdateProductResponse> UpdateProductAsync(CreateUpdateProductDto updateProduct,int id); 
    Task<DeleteProductDto> DeleteProductAsync(int id);

}

