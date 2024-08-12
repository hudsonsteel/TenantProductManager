using AutoMapper;
using TenantProductManager.Application.Dtos.Category;
using TenantProductManager.Application.Interfaces.Services;
using TenantProductManager.Domain.Interfaces.Repositories;
using Entity = TenantProductManager.Domain.Entities;

namespace TenantProductManager.Application.Services
{
    public sealed class CategoryService(ICategoryRepository categoryRepository, IMapper mapper) : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<CategoryResponseDto?>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            return categories.Select(c => _mapper.Map<CategoryResponseDto>(c));
        }

        public async Task<CategoryResponseDto?> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            return category == null ? null : _mapper.Map<CategoryResponseDto>(category);
        }

        public async Task<CategoryResponseDto?> CreateCategoryAsync(CreateCategoryRequestDto request)
        {
            var existingCategory = await _categoryRepository.GetCategoryByNameAndTenantIdAsync(request.Name, request.TenantId);

            if (existingCategory != null)
            {
                throw new InvalidOperationException($"A category with the name '{request.Name}' already exists for tenant {request.TenantId}.");
            }

            var category = _mapper.Map<Entity.Category>(request);
            var createdCategory = await _categoryRepository.AddAsync(category);
            return _mapper.Map<CategoryResponseDto>(createdCategory);
        }

        public async Task<bool> UpdateCategoryAsync(int id, UpdateCategoryRequestDto request)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return false;
            }

            _mapper.Map(request, category);
            await _categoryRepository.UpdateAsync(category);
            return true;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return false;
            }

            await _categoryRepository.DeleteAsync(id);
            return true;
        }
    }
}
