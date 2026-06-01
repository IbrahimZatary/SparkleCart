using Microsoft.IdentityModel.Tokens;
using SparkeApp.Models;
using SparkeApp.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SparkeApp.Services.Implementations;

public class JwtService(IConfiguration configuration)  : IJwtService
{
  
    public string GenerateToken(User user)
    {

            var claims = new List<Claim>
            {
          new(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

        var jwtSettings = configuration.GetSection("JwtSettings"); 
        var secretKey = jwtSettings["Secret"] ?? throw new Exception("JWT Secret not found");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(Convert.ToDouble(jwtSettings["ExpiryDays"] ?? "1")),
            SigningCredentials = credentials,
            Issuer = jwtSettings["Issuer"],
            Audience = jwtSettings["Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
