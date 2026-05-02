namespace SparkeApp.DTOs.Auth
{
    public class RegisterResponseDto
    {
        public string Name { get; set; } = default!;
        public string Email { get; set; }
        public string Message { get; internal set; }
    }
}
