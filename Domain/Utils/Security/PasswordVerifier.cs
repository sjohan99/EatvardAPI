using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Utils.Security;

public static class PasswordVerifier
{
    public static bool Verify(string unhashedPassword, string storedHashedPassword, string salt, PasswordHasher hasher)
    {
        var hashedPassword = hasher.Hash(unhashedPassword, salt);
        if (hashedPassword != storedHashedPassword)
        {
            return false;
        }
        return true;
    }
}
