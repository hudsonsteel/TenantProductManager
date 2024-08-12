namespace TenantProductManager.Api.Transports.Tenant
{
    public sealed record class TenantResponse
    {
        public TenantResponse(int id, string? name, int? parentTenantId, DateTime createdDate, bool isRoot, int? rootTenantId)
        {
            Id = id;
            Name = name;
            ParentTenantId = parentTenantId;
            CreatedDate = createdDate;
            IsRoot = isRoot;
            RootTenantId = rootTenantId;
        }

        public int Id { get; init; }
        public string? Name { get; init; }
        public int? ParentTenantId { get; init; }
        public DateTime CreatedDate { get; init; }
        public bool IsRoot { get; init; }
        public int? RootTenantId { get; init; }
    }
}
