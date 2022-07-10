using System.ComponentModel.DataAnnotations;

namespace EatvardAPI.Models
{
    public class UserAccount
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; } = null!; 

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!; // TODO require email format
    }
}
