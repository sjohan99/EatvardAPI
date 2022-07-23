using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class User
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
    public virtual string PasswordHash { get; set; } = null!;

    [MaxLength(200)]
    public virtual string PasswordSalt { get; set; } = null!;
}
