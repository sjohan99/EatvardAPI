using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EatvardAPI.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        public UserAccount Author { get; set; } = null!;

        public Restaurant? Restaurant { get; set; }

        public string? AlternativeName { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        public int Cost { get; set; }
    }
}
