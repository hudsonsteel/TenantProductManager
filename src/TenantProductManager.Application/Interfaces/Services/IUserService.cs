using System.Security.Claims;
using TenantProductManager.Application.Dtos.User;

namespace TenantProductManager.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserResponse> GetCurrentUserAsync(ClaimsPrincipal user);
    }
}
