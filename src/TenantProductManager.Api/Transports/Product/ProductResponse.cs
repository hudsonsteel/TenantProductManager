namespace TenantProductManager.Api.Transports.Product
{
    public sealed record class ProductResponse
    {
        public ProductResponse(int id, string name, int categoryId, int tenantId)
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
