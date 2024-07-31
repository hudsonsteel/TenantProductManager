using AutoMapper;
using TenantProductManager.Application.Dtos.Product;
using TenantProductManager.Application.Interfaces.Services;
using TenantProductManager.Domain.Entities;
using TenantProductManager.Domain.Interfaces.Repositories;

namespace TenantProductManager.Application.Services
{
    public sealed class ProductService(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        IMapper mapper) : IProductService
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Select(p => _mapper.Map<ProductResponseDto>(p));
        }

        public async Task<ProductResponseDto> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductResponseDto>(product);
        }

        public async Task<ProductResponseDto> CreateProductAsync(CreateProductRequestDto request)
        {
            var existingProduct = await _productRepository.GetProductByNameAndCategoryIdAsync(request.Name, request.CategoryId);

            if (existingProduct != null)
            {
                throw new InvalidOperationException($"A product with the name '{request.Name}' already exists for category {request.CategoryId}.");
            }

            var existingCategory = await _categoryRepository.GetByIdAsync(request.CategoryId);

            if (existingProduct != null)
            {
                throw new InvalidOperationException($"A category with the id '{request.CategoryId}' not exists.");
            }

            var product = _mapper.Map<Product>(request);
            product.TenantId = existingCategory.TenantId;

            var createdProduct = await _productRepository.AddAsync(product);
            return _mapper.Map<ProductResponseDto>(createdProduct);
        }

        public async Task<bool> UpdateProductAsync(int id, UpdateProductRequestDto request)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return false;
            }

            _mapper.Map(request, product);
            await _productRepository.UpdateAsync(product);
            return true;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return false;
            }

            await _productRepository.DeleteAsync(product.Id);
            return true;
        }
    }
}
