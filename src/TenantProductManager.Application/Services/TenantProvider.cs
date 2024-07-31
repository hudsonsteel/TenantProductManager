using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
using TenantProductManager.Domain.Interfaces.Services;

namespace TenantProductManager.Application.Services
{
    public class TenantProvider(IHttpContextAccessor httpContextAccessor) : ITenantProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public int GetTenantId()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null || !httpContext.Request.Headers.ContainsKey("Authorization"))
            {
                throw new UnauthorizedAccessException("No Authorization header present.");
            }

            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("No JWT token present.");
            }

            var tokenHandler = new JsonWebTokenHandler();
            var jwtToken = tokenHandler.ReadJsonWebToken(token);

            var claims = jwtToken.Claims;

            var tenantIdClaim = claims.FirstOrDefault(c => c.Type == "tenantId");

            if (tenantIdClaim == null || !int.TryParse(tenantIdClaim.Value, out int tenantId))
            {
                throw new UnauthorizedAccessException("TenantId claim is missing or invalid.");
            }

            return tenantId;
        }
    }
}
