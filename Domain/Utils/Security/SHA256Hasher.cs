using System.Security.Cryptography;
using System.Text;

namespace Domain.Utils.Security;

public class SHA256Hasher : PasswordHasher
{
    public override string Algorithm()
    {
        return "SHA256";
    }

    public override string Hash(string password, string salt = "")
    {
        using (var sha256 = SHA256.Create())
        {
            var saltedPassword = password + salt;
            byte[] saltedPasswordAsBytes = Encoding.UTF8.GetBytes(saltedPassword);
            return Convert.ToBase64String(sha256.ComputeHash(saltedPasswordAsBytes));
        }
    }
}
