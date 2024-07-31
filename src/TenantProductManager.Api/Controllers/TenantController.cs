using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TenantProductManager.Api.Transports.Tenant;
using TenantProductManager.Application.Dtos.Tenant;
using TenantProductManager.Application.Helpers;
using TenantProductManager.Application.Interfaces.Services;

namespace TenantProductManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TenantController(
        ITenantService tenantService,
        IUserService userService,
        IMapper mapper,
        IValidator<CreateTenantRequest> createTenantValidator,
        IValidator<UpdateTenantRequest> updateTenantValidator) : ControllerBase
    {
        private readonly ITenantService _tenantService = tenantService;
        private readonly IUserService _userService = userService;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<CreateTenantRequest> _createTenantValidator = createTenantValidator;
        private readonly IValidator<UpdateTenantRequest> _updateTenantValidator = updateTenantValidator;

        [HttpGet]
        [Authorize(Roles = ClaimHelper.Admin)]
        public async Task<IActionResult> GetAllTenants()
        {
            var tenants = await _tenantService.GetAllTenantsAsync();
            return Ok(tenants);
        }

        [Authorize(Roles = ClaimHelper.Admin)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTenantById(int id)
        {
            var tenant = await _tenantService.GetTenantByIdAsync(id);
            if (tenant == null)
                return NotFound();

            var user = await _userService.GetCurrentUserAsync(User);
            if (!user.IsAdmin && tenant.Id != user.TenantId)
                return Forbid();

            return Ok(tenant);
        }

        [HttpPost]
        [Authorize(Roles = ClaimHelper.Admin)]
        public async Task<IActionResult> CreateTenant([FromBody] CreateTenantRequest request)
        {
            var validationResult = await _createTenantValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var dto = _mapper.Map<CreateTenantRequestDto>(request);
            var tenant = await _tenantService.CreateTenantAsync(dto);
            return CreatedAtAction(nameof(GetTenantById), new { id = tenant.Id }, tenant);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = ClaimHelper.Admin)]
        public async Task<IActionResult> UpdateTenant(int id, [FromBody] UpdateTenantRequest request)
        {
            var validationResult = await _updateTenantValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var dto = _mapper.Map<UpdateTenantRequestDto>(request);
            var result = await _tenantService.UpdateTenantAsync(id, dto);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = ClaimHelper.Admin)]
        public async Task<IActionResult> DeleteTenant(int id)
        {
            var result = await _tenantService.DeleteTenantAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
