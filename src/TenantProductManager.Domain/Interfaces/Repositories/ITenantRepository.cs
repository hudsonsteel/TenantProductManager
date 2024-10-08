﻿using TenantProductManager.Domain.Entities;

namespace TenantProductManager.Domain.Interfaces.Repositories
{
    public interface ITenantRepository
    {
        Task<bool> ExistsByTenantIdOrRootTenantIdAsync(int? tenantId, int? rootTenantId);
        Task<IEnumerable<Tenant?>> GetAllAsync();
        Task<Tenant?> GetByIdAsync(int id);
        Task<Tenant?> AddAsync(Tenant tenant);
        Task UpdateAsync(Tenant tenant);
        Task DeleteAsync(int id);
    }
}
