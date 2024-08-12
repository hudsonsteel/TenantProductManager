using TenantProductManager.Domain.Entities;

namespace TenantProductManager.Domain.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<Product?> GetProductByNameAndCategoryIdAsync(string name, int categoryId);
        Task<IEnumerable<Product?>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<Product?> AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
    }
}
