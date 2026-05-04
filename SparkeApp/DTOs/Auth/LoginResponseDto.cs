namespace SparkeApp.DTOs.Auth;

public class LoginResponseDto
{
    public string AccessToken { get; internal set; } = default !;
    public int ExpiresIn { get; internal set; }
    public int UserId { get; internal set; }
    public string Email { get; internal set; }
}
