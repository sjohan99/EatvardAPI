using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Restaurant;
public class UpdateRestaurantDTO
{
    [MaxLength(200)]
    public string? Name { get; set; }

    [MaxLength(200)]
    public string? StreetAddress { get; set; }

    [MaxLength(20)]
    public string? StreetNumber { get; set; }

    [MaxLength(100)]
    public string? City { get; set; }

    [MaxLength(100)]
    public string? State { get; set; }

    [MaxLength(20)]
    public string? ZipCode { get; set; }
}
