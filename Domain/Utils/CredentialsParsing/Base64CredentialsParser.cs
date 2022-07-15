using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Utils.CredentialsParsing;

public class Base64CredentialsParser : CredentialsParser
{
    public Base64CredentialsParser(string credentials) : base(credentials)
    {
    }

    protected override string[] Parse(string? credentials)
    {
        var bytes = ConvertToBytes(credentials);
        string[] parsedCredentials = Encoding.UTF8.GetString(bytes).Split(":");
        if (parsedCredentials.Length != 2)
        {
            ParseSuccessful = false;
            return new string[] { "", "" };
        }
        ParseSuccessful = true;
        return parsedCredentials;
    }

    private byte[] ConvertToBytes(string? credentials)
    {
        if (credentials == null)
        {
            return Array.Empty<byte>();
        }
        return Convert.FromBase64String(credentials);
    }
}
