using System.ComponentModel.DataAnnotations;

namespace EatvardDataAccessLibrary.Models;

public class Restaurant
{
    [Key]
    public int Id { get; set; }

    [MaxLength(200)]
    public string Name { get; set; } = null!;

    public virtual Address Address { get; set; } = null!;
}
