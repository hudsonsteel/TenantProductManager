using TenantProductManager.Domain.Interfaces.Entities;

namespace TenantProductManager.Domain.Entities
{
    public class Category : IEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int TenantId { get; set; }
        public Tenant? Tenant { get; set; }

        public int? ParentCategoryId { get; set; }
        public Category? ParentCategory { get; set; }
        public ICollection<Category> SubCategories { get; set; } = [];

        public ICollection<Product> Products { get; set; } = [];

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
