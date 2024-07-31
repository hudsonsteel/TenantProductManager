namespace TenantProductManager.Domain.Interfaces.Entities
{
    public interface IEntity
    {
        int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
