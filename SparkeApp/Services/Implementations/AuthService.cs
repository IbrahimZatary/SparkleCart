using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using SparkeApp.Data;
using SparkeApp.DTOs.Auth;
using SparkeApp.Models;
using SparkeApp.Exceptions;
using SparkeApp.Services.Interfaces;


namespace SparkeApp.Services.Implementations;

public class AuthService(AppDbContext context, IJwtService jwtService) : IAuthService
{
    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto LoginRequest)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Email == LoginRequest.Email);

        if (user is null || !BCrypt.Net.BCrypt.Verify(LoginRequest.Password, user.PasswordHash))
            throw new UnauthorizedException("Invalid email or password.");
        
        var token = jwtService.GenerateToken(user);
        return new LoginResponseDto
        {
            AccessToken = token,
            ExpiresIn = 3600,
            UserId = user.Id,
            Email = user.Email   
        };
    }

    public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto RegisterRequest)
    {
        var existEmail = await context.Users
            .FirstOrDefaultAsync(u => u.Email == RegisterRequest.Email);

        if (existEmail != null)
            throw new ConflictException("The user already has an account");

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(RegisterRequest.Password);
        var NewUser = new User
        {
            Name = RegisterRequest.Name,
            Email = RegisterRequest.Email,
            PasswordHash = hashedPassword,
        };
        await context.Users.AddAsync(NewUser);
        await context.SaveChangesAsync();
 
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