using Microsoft.EntityFrameworkCore;
using TenantProductManager.Domain.Entities;
using TenantProductManager.Domain.Interfaces.Repositories;
using TenantProductManager.Infrastructure.DataBaseContext;

namespace TenantProductManager.Infrastructure.Repositories
{
    public class CategoryRepository(ApplicationDbContext context) : BaseRepository<Category>(context), ICategoryRepository
    {
        public override async Task<IEnumerable<Category?>> GetAllAsync()
        {
            return await _dbSet
                         .Include(c => c.ParentCategory)
                         .Include(c => c.SubCategories)
                         .AsNoTracking()
                         .ToListAsync();
        }

        public override async Task<Category?> GetByIdAsync(int id)
        {
            return await _dbSet
                         .Include(c => c.ParentCategory)
                         .Include(c => c.SubCategories)
                         .AsNoTracking()
                         .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category?> GetCategoryByNameAndTenantIdAsync(string name, int tenantId)
        {
            return await _context.Categories
                        .Where(c => c.Name == name && c.TenantId == tenantId)
                        .FirstOrDefaultAsync();
        }
    }
}
