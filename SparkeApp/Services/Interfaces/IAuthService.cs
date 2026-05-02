using SparkeApp.DTOs.Auth;
namespace SparkeApp.Services.Interfaces 
{
    public interface IAuthService
    {
        // For sign-up as new user 
        Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto RegisterRequest);

        // For Login 
        Task<LoginResponseDto> LoginAsync(LoginRequestDto LoginRequest);
    }
}
