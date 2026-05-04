using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SparkeApp.Models;

[Index(nameof(Email), IsUnique = true)] 
public class User
{
    public int Id { get; set; } 
    public string Name { get; set; } = default!;
    [Required]
    [EmailAddress] 
    public string Email { get; set; } = default!;
    [Required]
    public string PasswordHash { get; set; } =default!; 
    public Cart Cart { get; set; } = default!; 
    public ICollection<Order> Orders { get; set; } = [];  
}   