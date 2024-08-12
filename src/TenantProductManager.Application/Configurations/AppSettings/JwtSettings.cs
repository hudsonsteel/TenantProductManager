namespace TenantProductManager.Application.Configurations.AppSettings
{
    public sealed record class JwtSettings
    {
        public string? Issuer { get; init; }
        public string? Audience { get; init; }
        public string? SecretKey { get; init; }
        public int ExpireTimeToken { get; init; }

        public DateTime GetTokenExpirationDate() => DateTime.Now.AddHours(ExpireTimeToken);
    }
}
