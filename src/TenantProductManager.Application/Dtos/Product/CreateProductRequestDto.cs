namespace TenantProductManager.Application.Dtos.Product
{
    public sealed record class CreateProductRequestDto
    {
        public CreateProductRequestDto(string name, int categoryId)
        {
            Name = name;
            CategoryId = categoryId;
        }

        public string Name { get; init; }
        public int CategoryId { get; init; }
    }
}
