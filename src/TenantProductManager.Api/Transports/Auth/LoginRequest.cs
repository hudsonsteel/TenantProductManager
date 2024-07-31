namespace TenantProductManager.Api.Transports.Auth
{
    public sealed record class LoginRequest
    {
        public LoginRequest(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public string UserName { get; init; }
        public string Password { get; init; }
    }
}
