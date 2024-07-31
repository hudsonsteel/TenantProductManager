using AutoMapper;
using TenantProductManager.Api.Transports.Product;
using TenantProductManager.Application.Dtos.Product;

namespace TenantProductManager.Api.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductRequest, CreateProductRequestDto>()
                .ConstructUsing(dto => new CreateProductRequestDto
                (
                    dto.Name,
                    dto.CategoryId
                ));

            CreateMap<ProductResponseDto, ProductResponse>()
                .ConstructUsing(product => new ProductResponse(
                    product.Id,
                    product.Name,
                    product.CategoryId,
                    product.TenantId
                ));

            CreateMap<UpdateProductRequestDto, UpdateProductRequest>()
                .ConstructUsing(dto => new UpdateProductRequest
                (
                    dto.Name,
                    dto.CategoryId
                ));
        }
    }
}
