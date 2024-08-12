using TenantProductManager.Application.Dtos.Category;

namespace TenantProductManager.Application.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponseDto?>> GetAllCategoriesAsync();
        Task<CategoryResponseDto?> GetCategoryByIdAsync(int id);
        Task<CategoryResponseDto?> CreateCategoryAsync(CreateCategoryRequestDto request);
        Task<bool> UpdateCategoryAsync(int id, UpdateCategoryRequestDto request);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
