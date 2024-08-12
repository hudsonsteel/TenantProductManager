namespace TenantProductManager.Application.Dtos.Category
{
    public sealed record class CategoryResponseDto
    {
        public CategoryResponseDto(
            int id,
            string? name,
            int tenantId,
            int? parentCategoryId,
            CategoryResponseDto? parentCategory,
            IEnumerable<CategoryResponseDto>? subCategories)
        {
            Id = id;
            Name = name;
            TenantId = tenantId;
            ParentCategoryId = parentCategoryId;
            ParentCategory = parentCategory;
            SubCategories = subCategories;
        }

        public int Id { get; init; }
        public string? Name { get; init; }
        public int TenantId { get; init; }
        public int? ParentCategoryId { get; init; }
        public CategoryResponseDto? ParentCategory { get; init; }
        public IEnumerable<CategoryResponseDto>? SubCategories { get; init; }
    }
}
