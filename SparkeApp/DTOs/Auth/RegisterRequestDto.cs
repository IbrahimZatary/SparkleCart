using System.ComponentModel.DataAnnotations;

namespace SparkeApp.DTOs.Auth;

public class RegisterRequestDto
{
    [Required]
    public string Name { get; set; } = default!;
    [Required]
    [EmailAddress(ErrorMessage = "Please enter a valid email address (e.g., user@example.com)")]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}
