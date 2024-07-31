using AutoMapper;
using TenantProductManager.Application.Dtos.Product;
using TenantProductManager.Domain.Entities;

namespace TenantProductManager.Application.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductRequestDto, Product>();

            CreateMap<UpdateProductRequestDto, Product>();

            CreateMap<Product, ProductResponseDto>()
                .ConstructUsing(src => new ProductResponseDto(
                    src.Id,
                    src.Name,
                    src.CategoryId,
                    src.TenantId
                ));
        }
    }
}
