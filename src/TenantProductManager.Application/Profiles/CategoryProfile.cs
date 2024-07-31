using AutoMapper;
using TenantProductManager.Application.Dtos.Category;
using TenantProductManager.Domain.Entities;

namespace TenantProductManager.Application.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CreateCategoryRequestDto, Category>();

            CreateMap<UpdateCategoryRequestDto, Category>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Category, CategoryResponseDto>()
             .ForMember(dest => dest.ParentCategory,
                        opt => opt.MapFrom(src => src.ParentCategory))
             .ForMember(dest => dest.SubCategories,
                        opt => opt.MapFrom(src => src.SubCategories))
             .ConstructUsing(src => new CategoryResponseDto(
                 src.Id,
                 src.Name,
                 src.TenantId,
                 src.ParentCategoryId,
                 src.ParentCategory == null ? null : new CategoryResponseDto(
                     src.ParentCategory.Id,
                     src.ParentCategory.Name,
                     src.ParentCategory.TenantId,
                     src.ParentCategory.ParentCategoryId,
                     null,  // Not setting SubCategories here to avoid infinite recursion.
                     null   // Not include the SubCategories as it will lead to recursion issues.
                 ),
                 src.SubCategories == null ? null : src.SubCategories.Select(x => new CategoryResponseDto(
                     x.Id,
                     x.Name,
                     x.TenantId,
                     x.ParentCategoryId,
                     null,  // Not setting ParentCategory here to avoid infinite recursion.
                     null   // Not include the SubCategories as it will lead to recursion issues.
                 ))
             ));
        }
    }
}
