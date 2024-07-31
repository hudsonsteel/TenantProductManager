using Microsoft.EntityFrameworkCore;
using TenantProductManager.Domain.Entities;
using TenantProductManager.Domain.Interfaces.Repositories;
using TenantProductManager.Infrastructure.DataBaseContext;

namespace TenantProductManager.Infrastructure.Repositories
{
    public class ProductRepository(ApplicationDbContext context) : BaseRepository<Product>(context), IProductRepository
    {
        public async Task<Product> GetProductByNameAndCategoryIdAsync(string name, int categoryId)
        {
            return await _context.Products
             .Where(p => p.Name == name && p.CategoryId == categoryId)
             .FirstOrDefaultAsync();
        }
    }
}
