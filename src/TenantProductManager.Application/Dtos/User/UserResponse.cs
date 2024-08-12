namespace TenantProductManager.Application.Dtos.User
{
    public sealed record class UserResponse
    {
        public UserResponse(string id, string? name, string? email, int? tenantId, bool isAdmin)
        {
            Id = id;
            Name = name;
            Email = email;
            TenantId = tenantId;
            IsAdmin = isAdmin;
        }

        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public int? TenantId { get; set; }
        public bool IsAdmin { get; set; }
    }
}
