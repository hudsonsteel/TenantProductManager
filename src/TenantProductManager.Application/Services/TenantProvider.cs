using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;
using TenantProductManager.Domain.Interfaces.Services;

namespace TenantProductManager.Application.Services
{
    public class TenantProvider(IHttpContextAccessor httpContextAccessor) : ITenantProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public int GetTenantId()
        {
            var token = GetTokenFromHeaders();
            var claims = GetTokenClaims(token);
            return GetTenantIdFromClaims(claims);
        }

        private string GetTokenFromHeaders()
        {
            var httpContext = _httpContextAccessor.HttpContext
                              ?? throw new UnauthorizedAccessException("No Authorization header present.");

            if (!httpContext.Request.Headers.TryGetValue("Authorization", out var authHeader) ||
                !authHeader.Any())
            {
                throw new UnauthorizedAccessException("No Authorization header present.");
            }

            var token = authHeader.FirstOrDefault()?.Split(" ").Last();

            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("No JWT token present.");
            }

            return token;
        }

        private static IEnumerable<Claim> GetTokenClaims(string token)
        {
            var tokenHandler = new JsonWebTokenHandler();
            var jwtToken = tokenHandler.ReadJsonWebToken(token);
            return jwtToken.Claims;
        }

        private static int GetTenantIdFromClaims(IEnumerable<Claim> claims)
        {
            var tenantIdClaim = claims.FirstOrDefault(c => c.Type == "tenantId");

            if (tenantIdClaim == null || !int.TryParse(tenantIdClaim.Value, out int tenantId))
            {
                throw new UnauthorizedAccessException("TenantId claim is missing or invalid.");
            }

            return tenantId;
        }
    }
}
