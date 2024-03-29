﻿using System.Security.Cryptography;

namespace Domain.Utils.Security;

public static class SaltGenerator
{
    private static int saltLength = 32;
    public static string GetSalt()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(saltLength));
    }
}
