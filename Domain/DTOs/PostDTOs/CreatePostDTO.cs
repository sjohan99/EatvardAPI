using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs;
public record CreatePostDTO
{
    [Required]
    public int AuthorId { get; set; }

    public int? RestaurantId { get; set; }

    [Required]
    [Range(1,5)]
    public int Rating { get; set; }

    public int? Cost { get; set; }

    public string? AlternativeName { get; set; }

    public string? Text { get; set; }

}
