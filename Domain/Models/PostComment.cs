using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models;
public class PostComment
{
    [Key]
    public int Id { get; set; }

    public int? AuthorId { get; set; }

    public int? PostId { get; set; }

    [MaxLength(1000)]
    public string Content { get; set; } = null!;
}
