namespace TenantProductManager.Api.Transports.Auth
{
    public record class RegisterRequest
    {
        public RegisterRequest(string userName, string password, string email, bool isAdmin, int tenantId)
        {
            UserName = userName;
            Password = password;
            Email = email;
            IsAdmin = isAdmin;
            TenantId = tenantId;
        }

        public string UserName { get; init; }
        public string Password { get; init; }
        public string Email { get; init; }
        public bool IsAdmin { get; init; }
        public int TenantId { get; init; }
    }
}
