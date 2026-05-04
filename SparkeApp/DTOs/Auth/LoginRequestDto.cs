using System.ComponentModel.DataAnnotations;

namespace SparkeApp.DTOs.Auth;

public class LoginRequestDto
{
    [Required]
    public required string Email { get; set; }
    [Required]
    public string Password { get; set; } = default!;
}
