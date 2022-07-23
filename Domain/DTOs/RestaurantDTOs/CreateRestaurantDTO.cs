using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs;
public class CreateRestaurantDTO
{
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string StreetAddress { get; set; } = null!;

    [MaxLength(20)]
    public string StreetNumber { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string City { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string State { get; set; } = null!;

    [Required]
    [MaxLength(20)]
    public string ZipCode { get; set; } = null!;
}
