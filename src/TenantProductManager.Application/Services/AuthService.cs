using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TenantProductManager.Application.Configurations.AppSettings;
using TenantProductManager.Application.Helpers;
using TenantProductManager.Application.Interfaces.Services;
using TenantProductManager.Domain.Entities;
using TenantProductManager.Domain.Interfaces.Repositories;

namespace TenantProductManager.Application.Services
{
    public class AuthService(
        IOptions<JwtSettings> jwtSettings,
        IUserRepository userRepository,
        ITenantService tenantService) : IAuthService
    {
        private readonly JwtSettings _jwtSettings = jwtSettings.Value;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ITenantService _tenantService = tenantService;

        public async Task<string?> AuthenticateAsync(string username, string password)
        {
            var userEntity = await _userRepository.GetUserByUsernameAsync(username);

            if (userEntity != null && VerifyPassword(userEntity.PasswordHash, password))
            {
                return GenerateJwtToken(userEntity.Name, userEntity.IsAdmin, userEntity.TenantId);
            }

            return null;
        }

        public async Task<bool> RegisterUserAsync(string username, string password, string email, bool isAdmin, int tenantId)
        {
            var existingTenant = await _tenantService.GetTenantByIdAsync(tenantId);

            if (existingTenant == null)
            {
                throw new InvalidOperationException($"There is not tennat with ID {tenantId} registered.");
            }

            var existingUser = await _userRepository.GetByUsernameOrEmailAsync(username, email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("A user with the same username or email already exists.");
            }

            var hashedPassword = HashPassword(password);

            var userEntity = new User
            {
                Name = username,
                Email = email,
                PasswordHash = hashedPassword,
                IsAdmin = isAdmin,
                TenantId = tenantId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var result = await _userRepository.AddAsync(userEntity);

            return result != null;
        }

        private static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private static bool VerifyPassword(string? storedHash, string? password)
        {
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }

        private string GenerateJwtToken(string? username, bool isAdmin, int? tenantId)
        {
            ArgumentNullException.ThrowIfNull(username, nameof(username));
            ArgumentNullException.ThrowIfNull(tenantId, nameof(tenantId));
            ArgumentNullException.ThrowIfNull(_jwtSettings.SecretKey, nameof(_jwtSettings.SecretKey));

            string tenantIdValue = tenantId.Value.ToString();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("tenantId", tenantIdValue)
            };

            if (isAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, ClaimHelper.Admin));
            }

            var key = new SymmetricSecurityKey(Convert.FromBase64String(_jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: _jwtSettings.GetTokenExpirationDate(),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
