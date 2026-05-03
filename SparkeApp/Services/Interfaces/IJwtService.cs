using SparkeApp.Models;

namespace SparkeApp.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}