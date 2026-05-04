using SparkeApp.DTOs.Auth;
namespace SparkeApp.Services.Interfaces;

public interface IAuthService
{

    Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto RegisterRequest);

    Task<LoginResponseDto> LoginAsync(LoginRequestDto LoginRequest);
}
