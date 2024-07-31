namespace TenantProductManager.Application.Dtos.Product
{
    public sealed record class UpdateProductRequestDto
    {
        public UpdateProductRequestDto(int id, string name, int categoryId)
        {
            Id = id;
            Name = name;
            CategoryId = categoryId;
        }

        public int Id { get; init; }
        public string Name { get; init; }
        public int CategoryId { get; init; }
    }
}
