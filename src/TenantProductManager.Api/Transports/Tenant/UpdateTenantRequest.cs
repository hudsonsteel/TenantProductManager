namespace TenantProductManager.Api.Transports.Tenant
{
    public sealed record class UpdateTenantRequest
    {
        public string Name { get; init; }
        public int? ParentTenantId { get; init; }
        public int? RootTenantId { get; init; }
        public bool IsRoot { get; init; }

        public UpdateTenantRequest(string name, int? parentTenantId, int? rootTenantId, bool isRoot)
        {
            Name = name;
            ParentTenantId = parentTenantId;
            RootTenantId = rootTenantId;
            IsRoot = isRoot;
        }
    }
}
