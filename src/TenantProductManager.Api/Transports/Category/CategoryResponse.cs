namespace TenantProductManager.Api.Transports.Category
{
    public sealed record class CategoryResponse
    {
        public CategoryResponse(int id, string? name, int tenantId, int? parentCategoryId)
        {
            Id = id;
            Name = name;
            TenantId = tenantId;
            ParentCategoryId = parentCategoryId;
        }

        public int Id { get; init; }
        public string? Name { get; init; }
        public int TenantId { get; init; }
        public int? ParentCategoryId { get; init; }
    }
}
