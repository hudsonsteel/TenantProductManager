using AutoMapper;
using TenantProductManager.Api.Transports.Tenant;
using TenantProductManager.Application.Dtos.Tenant;
using TenantProductManager.Domain.Entities;

namespace TenantProductManager.Api.Mappers
{
    public class TenantProfile : Profile
    {
        public TenantProfile()
        {
            CreateMap<CreateTenantRequest, CreateTenantRequestDto>()
             .ConstructUsing(src => new CreateTenantRequestDto(
                 src.Name,
                 src.ParentTenantId,
                 src.RootTenantId,
                 src.IsRoot
             ));

            CreateMap<Tenant, TenantResponse>()
                .ConstructUsing(src => new TenantResponse(
                    src.Id,
                    src.Name,
                    src.ParentTenantId,
                    src.CreatedAt,
                    src.IsRoot,
                    src.RootTenantId
                ));

            CreateMap<UpdateTenantRequest, UpdateTenantRequestDto>()
                .ConstructUsing(src => new UpdateTenantRequestDto(
                    src.Name,
                    src.ParentTenantId,
                    src.RootTenantId,
                    src.IsRoot
                ));
        }
    }
}
