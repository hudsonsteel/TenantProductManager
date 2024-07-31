using TenantProductManager.Domain.Interfaces.Entities;

namespace TenantProductManager.Domain.Entities
{
    public class Tenant : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? ParentTenantId { get; set; }
        public Tenant ParentTenant { get; set; }
        public ICollection<Tenant> Children { get; set; } = new List<Tenant>();

        public bool IsRoot { get; set; }

        public int? RootTenantId { get; set; }
        public Tenant RootTenant { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();

        public ICollection<Category> Categories { get; set; } = new List<Category>();
        public ICollection<Product> Products { get; set; } = new List<Product>();

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
