using FluentValidation;
using TenantProductManager.Api.Transports.Category;

namespace TenantProductManager.Api.Validations.Category
{
    public class UpdateCategoryRequestValidator : AbstractValidator<UpdateCategoryRequest>
    {
        public UpdateCategoryRequestValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name is required.")
            .Length(1, 255).WithMessage("Category name must be between 1 and 255 characters.");

            RuleFor(x => x.TenantId)
                .GreaterThan(0).WithMessage("Tenant ID must be a positive number.");

            RuleFor(x => x.ParentCategoryId)
                .GreaterThan(0).When(x => x.ParentCategoryId.HasValue)
                .WithMessage("Parent Category ID must be a positive number if provided.");
        }
    }
}
