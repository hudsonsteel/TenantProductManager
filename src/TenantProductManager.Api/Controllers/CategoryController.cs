using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TenantProductManager.Api.Transports.Category;
using TenantProductManager.Application.Dtos.Category;
using TenantProductManager.Application.Interfaces.Services;

namespace TenantProductManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController(
        ICategoryService categoryService,
        IValidator<CreateCategoryRequest> createCategoryValidator,
        IValidator<UpdateCategoryRequest> updateCategoryValidator,
        IMapper mapper) : ControllerBase
    {
        private readonly ICategoryService _categoryService = categoryService;
        private readonly IValidator<CreateCategoryRequest> _createCategoryValidator = createCategoryValidator;
        private readonly IValidator<UpdateCategoryRequest> _updateCategoryValidator = updateCategoryValidator;
        private readonly IMapper _mapper = mapper;

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryResponse>>> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResponse>> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [Authorize]
        [HttpPost("")]
        public async Task<ActionResult<CategoryResponse>> CreateCategory(CreateCategoryRequest request)
        {
            var result = await _createCategoryValidator.ValidateAsync(request);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var categoryDto = _mapper.Map<CreateCategoryRequestDto>(request);
            var createdCategory = await _categoryService.CreateCategoryAsync(categoryDto);
            return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory?.Id }, createdCategory);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(int id, UpdateCategoryRequest request)
        {
            if (id <= 0)
            {
                return BadRequest("Category ID mismatch.");
            }

            var result = await _updateCategoryValidator.ValidateAsync(request);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var categoryDto = _mapper.Map<UpdateCategoryRequestDto>(request);
            var success = await _categoryService.UpdateCategoryAsync(id, categoryDto);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var success = await _categoryService.DeleteCategoryAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
