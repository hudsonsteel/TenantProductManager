using Microsoft.EntityFrameworkCore;
using TenantProductManager.Domain.Entities;
using TenantProductManager.Domain.Interfaces.Repositories;
using TenantProductManager.Infrastructure.DataBaseContext;

namespace TenantProductManager.Infrastructure.Repositories
{
    public class TenantRepository(ApplicationDbContext context) : BaseRepository<Tenant>(context), ITenantRepository
    {
        public async Task<bool> ExistsByTenantIdOrRootTenantIdAsync(int? tenantId, int? rootTenantId)
        {
            return await _context.Tenants
                .AnyAsync(t => (tenantId != null && t.ParentTenantId == tenantId) ||
                               (rootTenantId != null && t.RootTenantId == rootTenantId));
        }
    }
}
