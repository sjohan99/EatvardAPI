using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EatvardAPI.Models
{
    public class UserAuthentication
    {
        [Key]
        [ForeignKey("UserAccount")]
        public string UserAccountId { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public string PasswordSalt { get; set; } = null!;
    }
}
