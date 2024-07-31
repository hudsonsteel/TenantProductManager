using FluentValidation;
using TenantProductManager.Api.Transports.Auth;

namespace TenantProductManager.Api.Validations.Auth
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required.")
                .Length(1, 50).WithMessage("Username must be between 1 and 50 characters.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .Length(6, 100).WithMessage("Password must be between 6 and 100 characters.");
        }
    }
}
