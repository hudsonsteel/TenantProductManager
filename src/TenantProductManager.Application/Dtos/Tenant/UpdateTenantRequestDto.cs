namespace TenantProductManager.Application.Dtos.Tenant
{
    public sealed record class UpdateTenantRequestDto
    {
        public string Name { get; init; }
        public int? ParentTenantId { get; init; }
        public int? RootTenantId { get; init; }
        public bool IsRoot { get; init; }

        public UpdateTenantRequestDto(string name, int? parentTenantId, int? rootTenantId, bool isRoot)
        {
            Name = name;
            ParentTenantId = parentTenantId;
            RootTenantId = rootTenantId;
            IsRoot = isRoot;
        }
    }
}
