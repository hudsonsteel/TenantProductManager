using TenantProductManager.Application.Dtos.Product;

namespace TenantProductManager.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync();
        Task<ProductResponseDto> GetProductByIdAsync(int id);
        Task<ProductResponseDto> CreateProductAsync(CreateProductRequestDto createProductDto);
        Task<bool> UpdateProductAsync(int id, UpdateProductRequestDto updateProductDto);
        Task<bool> DeleteProductAsync(int id);
    }
}
