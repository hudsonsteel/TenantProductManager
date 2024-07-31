using AutoMapper;
using TenantProductManager.Application.Dtos.Tenant;
using TenantProductManager.Domain.Entities;

namespace TenantProductManager.Application.Profiles
{
    public class TenantProfile : Profile
    {
        public TenantProfile()
        {
            CreateMap<Tenant, TenantResponseDto>()
            .ConstructUsing(src => new TenantResponseDto(
                src.Id,
                src.Name,
                src.ParentTenantId,
                src.IsRoot,
                src.RootTenantId
            ));

            CreateMap<CreateTenantRequestDto, Tenant>();

            CreateMap<UpdateTenantRequestDto, Tenant>();
        }
    }
}
