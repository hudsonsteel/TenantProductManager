using System.Security.Claims;
using TenantProductManager.Application.Dtos.User;
using TenantProductManager.Application.Interfaces.Services;
using TenantProductManager.Domain.Interfaces.Repositories;

namespace TenantProductManager.Application.Services
{
    public sealed class UserService(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<UserResponse> GetCurrentUserAsync(ClaimsPrincipal user)
        {
            var userIdString = user.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString))
            {
                throw new UnauthorizedAccessException("User ID is missing from claims.");
            }

            if (!int.TryParse(userIdString, out var userId))
            {
                throw new InvalidOperationException("Invalid user ID format.");
            }

            var userEntity = await _userRepository.GetByIdAsync(userId);

            if (userEntity == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            var userResponse = new UserResponse
            (
                userEntity.Id.ToString(),
                userEntity.Name,
                userEntity.Email,
                userEntity.TenantId,
                userEntity.IsAdmin
            );

            return userResponse;
        }
    }
}