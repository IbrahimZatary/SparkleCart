using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using SparkeApp.Data;
using SparkeApp.DTOs.Auth;
using SparkeApp.Models;
using SparkeApp.Services.Interfaces;


namespace SparkeApp.Services.Implementations
{
    public class AuthService(AppDbContext context) : IAuthService
    {
        // Login - No JWT
        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto LoginRequest)
        {
            // Get user from DB by email
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == LoginRequest.Email);

            // Validate user & password
            if (user == null || !BCrypt.Net.BCrypt.Verify(LoginRequest.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            return new LoginResponseDto
            {
                Email = user.Email,
                Message = "Login successful"
                // Token removed
                // ExpiresIn removed
            };
        }

        // Register
        public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto RegisterRequest)
        {
            // Check if user email already exists
            var existEmail = await context.Users
                .FirstOrDefaultAsync(u => u.Email == RegisterRequest.Email);

            if (existEmail != null)
                throw new ArgumentException("The user already has an account");

            // Hash the password 
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(RegisterRequest.Password);
            var NewUser = new User
            {
                Name = RegisterRequest.Name,
                Email = RegisterRequest.Email,
                PasswordHash = hashedPassword,
            };

            // Add user to database 
            await context.Users.AddAsync(NewUser);
            await context.SaveChangesAsync();

            // Create cart for new user
            var cart = new Cart
            {
                UserId = NewUser.Id,
            };
            await context.Carts.AddAsync(cart);
            await context.SaveChangesAsync();

            return new RegisterResponseDto
            {
                Name = NewUser.Name,
                Email = NewUser.Email,
                Message = $"Welcome {NewUser.Name} to SparkleCart! Your cart is ready."
            };
        }
    }
}