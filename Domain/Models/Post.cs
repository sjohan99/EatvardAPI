using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Post
{
    [Key]
    public int Id { get; set; }

    public virtual User Author { get; set; } = null!;

    public virtual Restaurant? Restaurant { get; set; }

    public string? AlternativeName { get; set; }

    [Range(1, 5)]
    public int Rating { get; set; }

    public int Cost { get; set; }

    public virtual DateTime CreatedAt { get; set; }
}
