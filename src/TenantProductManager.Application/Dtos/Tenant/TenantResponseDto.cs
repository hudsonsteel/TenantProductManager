namespace TenantProductManager.Application.Dtos.Tenant
{
    public sealed record class TenantResponseDto
    {
        public TenantResponseDto(int id, string? name, int? parentTenantId, bool isRoot, int? rootTenantId)
        {
            Id = id;
            Name = name;
            ParentTenantId = parentTenantId;
            IsRoot = isRoot;
            RootTenantId = rootTenantId;
        }

        public int Id { get; init; }
        public string? Name { get; init; }
        public int? ParentTenantId { get; init; }
        public bool IsRoot { get; init; }
        public int? RootTenantId { get; init; }
    }
}
