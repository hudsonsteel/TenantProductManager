﻿namespace TenantProductManager.Api.Transports.Category
{
    public sealed record class CreateCategoryRequest
    {
        public CreateCategoryRequest(string name, int tenantId, int? parentCategoryId)
        {
            Name = name;
            TenantId = tenantId;
            ParentCategoryId = parentCategoryId;
        }

        public string Name { get; init; }
        public int TenantId { get; init; }
        public int? ParentCategoryId { get; init; }
    }
}
