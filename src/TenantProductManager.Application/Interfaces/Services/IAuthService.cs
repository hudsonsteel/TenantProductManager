namespace TenantProductManager.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<string?> AuthenticateAsync(string username, string password);
        Task<bool> RegisterUserAsync(string username, string password, string email, bool isAdmin, int tenantId);
    }
}
