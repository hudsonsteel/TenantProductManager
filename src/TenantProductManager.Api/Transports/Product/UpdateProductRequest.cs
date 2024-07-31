namespace TenantProductManager.Api.Transports.Product
{
    public sealed record class UpdateProductRequest
    {
        public UpdateProductRequest(string name, int categoryId)
        {
            Name = name;
            CategoryId = categoryId;
        }

        public string Name { get; init; }
        public int CategoryId { get; init; }
    }
}
