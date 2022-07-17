using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EatvardDataAccessLibrary.Models;

[Index(nameof(Email), IsUnique = true)]
public class UserAccount
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string FirstName { get; set; } = null!;

    [MaxLength(100)]
    public string LastName { get; set; } = null!;

    [MaxLength(200)]
    public string Email { get; set; } = null!; // TODO require email format

    [MaxLength(500)]
    public string PasswordHash { get; set; } = null!;

    [MaxLength(200)]
    public string PasswordSalt { get; set; } = null!;
}
