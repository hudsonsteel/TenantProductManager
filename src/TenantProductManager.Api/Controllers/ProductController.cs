using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TenantProductManager.Api.Transports.Product;
using TenantProductManager.Application.Dtos.Product;
using TenantProductManager.Application.Interfaces.Services;

namespace TenantProductManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IProductService productService, IValidator<CreateProductRequest> createProductValidator, IValidator<UpdateProductRequest> updateProductValidator, IMapper mapper) : ControllerBase
    {
        private readonly IProductService _productService = productService;
        private readonly IValidator<CreateProductRequest> _createProductValidator = createProductValidator;
        private readonly IValidator<UpdateProductRequest> _updateProductValidator = updateProductValidator;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetAll()
        {
            var products = await _productService.GetAllProductsAsync();
            var productDtos = _mapper.Map<IEnumerable<ProductResponseDto>>(products);
            return Ok(productDtos);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponseDto>> GetById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var productDto = _mapper.Map<ProductResponseDto>(product);
            return Ok(productDto);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ProductResponseDto>> Create([FromBody] CreateProductRequest request)
        {
            var result = await _createProductValidator.ValidateAsync(request);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var product = _mapper.Map<CreateProductRequestDto>(request);
            var createdProduct = await _productService.CreateProductAsync(product);
            var productDto = _mapper.Map<ProductResponseDto>(createdProduct);
            return CreatedAtAction(nameof(GetById), new { id = productDto.Id }, productDto);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateProductRequest request)
        {
            var result = await _updateProductValidator.ValidateAsync(request);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var product = _mapper.Map<UpdateProductRequestDto>(request);
            var success = await _productService.UpdateProductAsync(id, product);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _productService.DeleteProductAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
