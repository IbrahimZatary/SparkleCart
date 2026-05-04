using Microsoft.AspNetCore.Mvc;
using SparkeApp.Data;
using SparkeApp.DTOs.Auth;
using SparkeApp.Services.Interfaces;

namespace SparkeApp.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(IAuthService authService ) : ControllerBase
{

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto LoginRequest)
    {
            var result = await authService.LoginAsync(LoginRequest);
            return Ok(result);
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto RegisterRequest)
    {
        var result = await authService.RegisterAsync(RegisterRequest);
        return Ok(result);
    }
}