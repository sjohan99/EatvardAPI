using EatvardDataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EatvardAPI.Models
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(200)]
        public string Name { get; set; } = null!;

        public Address Address { get; set; } = null!;
    }
}
