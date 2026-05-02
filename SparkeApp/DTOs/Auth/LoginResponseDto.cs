namespace SparkeApp.DTOs.Auth
{
    public class LoginResponseDto
    {
        // For now, we will not implement JWT token generation, but we can include properties for future use
        //public string Token { get; set; }
        //public DateTime Expiration { get; set; }
        public required string Email { get; set; }
        public required string Message { get; set; }
       // For JWT 
        //public string Token { get; internal set; } = default !;
        //public int ExpiresIn { get; internal set; }
    }
}
