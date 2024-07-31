using TenantProductManager.Domain.Entities;

namespace TenantProductManager.Domain.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category> GetCategoryByNameAndTenantIdAsync(string name, int tenantId);
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task<Category> AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(int id);
    }
}
