using AutoMapper;
using TenantProductManager.Api.Transports.Category;
using TenantProductManager.Application.Dtos.Category;

namespace TenantProductManager.Api.Mappers
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CreateCategoryRequest, CreateCategoryRequestDto>()
               .ConstructUsing(dto => new CreateCategoryRequestDto
               (
                   dto.Name,
                   dto.TenantId,
                   dto.ParentCategoryId
               ));

            CreateMap<UpdateCategoryRequest, UpdateCategoryRequestDto>()
              .ConstructUsing(dto => new UpdateCategoryRequestDto
              (
                  dto.Name,
                  dto.TenantId,
                  dto.ParentCategoryId
              ));

            CreateMap<CategoryResponseDto, CategoryResponse>()
             .ConstructUsing(dto => new CategoryResponse
             (
                 dto.Id,
                 dto.Name,
                 dto.TenantId,
                 dto.ParentCategoryId
             ));
        }
    }
}
