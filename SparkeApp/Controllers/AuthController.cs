using Microsoft.AspNetCore.Mvc;
using SparkeApp.Data;
using SparkeApp.DTOs.Auth;
using SparkeApp.Services.Interfaces;

namespace SparkeApp.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{

    // Login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto LoginRequest)
    {
        try
        {
            var result = await authService.LoginAsync(LoginRequest);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    // Register
    [HttpPost("sign-up")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto RegisterRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await authService.RegisterAsync(RegisterRequest);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}