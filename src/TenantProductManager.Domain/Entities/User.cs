using TenantProductManager.Domain.Interfaces.Entities;

namespace TenantProductManager.Domain.Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public int? TenantId { get; set; }
        public bool IsAdmin { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Tenant? Tenant { get; set; }
    }
}
