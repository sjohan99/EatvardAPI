using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EatvardAPI.Models
{
    public class UserAuthentication
    {
        [Key]
        [ForeignKey("UserAccount")]
        public string UserAccountId { get; set; } = null!;

        [MaxLength(500)]
        public string PasswordHash { get; set; } = null!;

        [MaxLength(200)]
        public string PasswordSalt { get; set; } = null!;
    }
}
