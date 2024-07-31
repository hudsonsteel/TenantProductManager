using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TenantProductManager.Api.Transports.Auth;
using TenantProductManager.Application.Helpers;
using TenantProductManager.Application.Interfaces.Services;

namespace TenantProductManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(
        IAuthService authService,
        IValidator<LoginRequest> loginRequestValidator,
        IValidator<RegisterRequest> registerRequestValidator) : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        private readonly IValidator<LoginRequest> _loginRequestValidator = loginRequestValidator;
        private readonly IValidator<RegisterRequest> _registerRequestValidator = registerRequestValidator;


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var validationResult = await _loginRequestValidator.ValidateAsync(loginRequest);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var token = await _authService.AuthenticateAsync(loginRequest.UserName, loginRequest.Password);

            if (token == null)
                return Unauthorized("Invalid credentials.");

            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        [Authorize(Roles = ClaimHelper.Admin)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            var validationResult = await _registerRequestValidator.ValidateAsync(registerRequest);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var success = await _authService.RegisterUserAsync(registerRequest.UserName, registerRequest.Password, registerRequest.Email, registerRequest.IsAdmin, registerRequest.TenantId);

            if (!success)
                return BadRequest("User registration failed.");

            return Ok("User registered successfully.");
        }
    }
}
