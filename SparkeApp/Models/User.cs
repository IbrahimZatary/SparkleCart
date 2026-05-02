using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SparkeApp.Models;

[Index(nameof(Email), IsUnique = true)] 
public class User
{
    public int Id { get; set; } // PK
    public string Name { get; set; } = default!;
    [Required]
    [EmailAddress] 
    public string Email { get; set; } = default!;
    [Required]
    public string PasswordHash { get; set; } =default!; // Store hashed password for security
    // each user have many orders  
    // Relations 
    public Cart Cart { get; set; } = default!; // one to one relationship with cart
    public ICollection<Order> Orders { get; set; } = [];  // ONE User has MANY Orders 
}   