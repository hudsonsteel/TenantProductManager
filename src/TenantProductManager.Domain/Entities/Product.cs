using TenantProductManager.Domain.Interfaces.Entities;

namespace TenantProductManager.Domain.Entities
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public int TenantId { get; set; }
        public Tenant? Tenant { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
