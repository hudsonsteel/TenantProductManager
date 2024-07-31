using AutoMapper;
using TenantProductManager.Application.Dtos.Tenant;
using TenantProductManager.Application.Interfaces.Services;
using TenantProductManager.Domain.Entities;
using TenantProductManager.Domain.Interfaces.Repositories;

namespace TenantProductManager.Application.Services
{
    public class TenantService(ITenantRepository tenantRepository, IMapper mapper) : ITenantService
    {
        private readonly ITenantRepository _tenantRepository = tenantRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<TenantResponseDto>> GetAllTenantsAsync()
        {
            var tenants = await _tenantRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TenantResponseDto>>(tenants);
        }

        public async Task<TenantResponseDto> GetTenantByIdAsync(int id)
        {
            var tenant = await _tenantRepository.GetByIdAsync(id);
            return _mapper.Map<TenantResponseDto>(tenant);
        }

        public async Task<TenantResponseDto> CreateTenantAsync(CreateTenantRequestDto request)
        {
            var tenant = _mapper.Map<Tenant>(request);
            await _tenantRepository.AddAsync(tenant);
            return _mapper.Map<TenantResponseDto>(tenant);
        }

        public async Task<bool> UpdateTenantAsync(int id, UpdateTenantRequestDto request)
        {
            var tenant = await _tenantRepository.GetByIdAsync(id);
            if (tenant == null)
            {
                return false;
            }

            _mapper.Map(request, tenant);
            await _tenantRepository.UpdateAsync(tenant);
            return true;
        }

        public async Task<bool> DeleteTenantAsync(int id)
        {
            var tenant = await _tenantRepository.GetByIdAsync(id);
            if (tenant == null)
            {
                return false;
            }

            await _tenantRepository.DeleteAsync(tenant.Id);
            return true;
        }
    }
}
