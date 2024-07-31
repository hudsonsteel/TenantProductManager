using TenantProductManager.Application.Dtos.Tenant;

namespace TenantProductManager.Application.Interfaces.Services
{
    public interface ITenantService
    {
        Task<IEnumerable<TenantResponseDto>> GetAllTenantsAsync();
        Task<TenantResponseDto> GetTenantByIdAsync(int id);
        Task<TenantResponseDto> CreateTenantAsync(CreateTenantRequestDto request);
        Task<bool> UpdateTenantAsync(int id, UpdateTenantRequestDto request);
        Task<bool> DeleteTenantAsync(int id);
    }
}
