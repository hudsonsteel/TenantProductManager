namespace TenantProductManager.Application.Dtos.Product
{
    public sealed record class ProductResponseDto
    {
        public ProductResponseDto(int id, string name, int categoryId, int tenantId)
        {
            Id = id;
            Name = name;
            CategoryId = categoryId;
            TenantId = tenantId;
        }

        public int Id { get; init; }
        public string Name { get; init; }
        public int CategoryId { get; init; }
        public int TenantId { get; init; }
    }
}
