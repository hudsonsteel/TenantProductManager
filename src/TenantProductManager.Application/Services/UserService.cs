using System.Security.Claims;
using TenantProductManager.Application.Dtos.User;
using TenantProductManager.Application.Interfaces.Services;
using TenantProductManager.Domain.Entities;
using TenantProductManager.Domain.Interfaces.Repositories;

namespace TenantProductManager.Application.Services
{
    public sealed class UserService(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<UserResponse> GetCurrentUserAsync(ClaimsPrincipal user)
        {
            var userId = ExtractUserId(user);
            var userEntity = await GetUserEntityAsync(userId);
            return MapToUserResponse(userEntity);
        }

        private static int ExtractUserId(ClaimsPrincipal user)
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

            return userId;
        }

        private async Task<User> GetUserEntityAsync(int userId)
        {
            var userEntity = await _userRepository.GetByIdAsync(userId);

            if (userEntity == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            return userEntity;
        }

        private static UserResponse MapToUserResponse(User userEntity)
        {
            return new UserResponse
            (
                userEntity.Id.ToString(),
                userEntity.Name,
                userEntity.Email,
                userEntity.TenantId,
                userEntity.IsAdmin
            );
        }
    }
}