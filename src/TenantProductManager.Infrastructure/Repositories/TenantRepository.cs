using TenantProductManager.Domain.Entities;
using TenantProductManager.Domain.Interfaces.Repositories;
using TenantProductManager.Infrastructure.DataBaseContext;

namespace TenantProductManager.Infrastructure.Repositories
{
    public class TenantRepository(ApplicationDbContext context) : BaseRepository<Tenant>(context), ITenantRepository
    {
    }
}
