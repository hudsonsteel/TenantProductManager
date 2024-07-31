using FluentValidation;
using TenantProductManager.Api.Transports.Product;

namespace TenantProductManager.Api.Validations.Product
{
    public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator()
        {
            RuleFor(dto => dto.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .Length(1, 100).WithMessage("Product name must be between 1 and 100 characters.");

            RuleFor(dto => dto.CategoryId)
                .GreaterThan(0).WithMessage("CategoryId must be a positive integer.");
        }
    }
}
