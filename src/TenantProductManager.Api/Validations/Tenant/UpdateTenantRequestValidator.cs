using FluentValidation;
using TenantProductManager.Api.Transports.Tenant;

namespace TenantProductManager.Api.Validations.Tenant
{
    public class UpdateTenantRequestValidator : AbstractValidator<UpdateTenantRequest>
    {
        public UpdateTenantRequestValidator()
        {
            RuleFor(request => request.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(3, 100).WithMessage("Name must be between 3 and 100 characters.");

            RuleFor(request => request.ParentTenantId)
                .GreaterThan(0)
                .When(request => !request.IsRoot)
                .WithMessage("ParentTenantId must be greater than 0 if provided and not a root tenant.");

            RuleFor(request => request.RootTenantId)
                .GreaterThan(0)
                .When(request => request.IsRoot)
                .WithMessage("RootTenantId must be greater than 0 for root tenants.");
        }
    }
}
