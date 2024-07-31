using FluentValidation;
using TenantProductManager.Api.Transports.Tenant;

namespace TenantProductManager.Api.Validations.Tenant
{
    public class CreateTenantRequestValidator : AbstractValidator<CreateTenantRequest>
    {
        public CreateTenantRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(255).WithMessage("Name must be less than 255 characters.");

            RuleFor(x => x.ParentTenantId)
                .Must((request, parentTenantId) => !request.IsRoot || !parentTenantId.HasValue)
                .WithMessage("Root tenants cannot have a ParentTenantId.");

            RuleFor(x => x.RootTenantId)
                .Must((request, rootTenantId) =>
                    !request.IsRoot || (rootTenantId.HasValue && rootTenantId.Value > 0) ||
                    (!rootTenantId.HasValue && request.IsRoot))
                .WithMessage("Root tenants must have a RootTenantId greater than 0 or RootTenantId can be null.");

            RuleFor(x => x.IsRoot)
                .NotNull().WithMessage("IsRoot flag must be specified.");
        }
    }
}
