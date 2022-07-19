using System.ComponentModel.DataAnnotations;

namespace EatvardDataAccessLibrary.Models;

public class Address
{
    [Key]
    public int Id { get; set; }

    [MaxLength(200)]
    public string StreetAddress { get; set; } = null!;

    [MaxLength(20)]
    public string StreetNumber { get; set; } = null!;

    [MaxLength(100)]
    public string City { get; set; } = null!;

    [MaxLength(100)]
    public string State { get; set; } = null!;

    [MaxLength(20)]
    public string ZipCode { get; set; } = null!;
}
