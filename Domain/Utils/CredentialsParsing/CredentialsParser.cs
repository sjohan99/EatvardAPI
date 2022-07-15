using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Utils;

public abstract class CredentialsParser
{
    public readonly string Identity;
    public readonly string Password;
    public bool ParseSuccessful;

    public CredentialsParser(string? credentials)
    {
        string[] parsedCredentials = Parse(credentials);
        Identity = parsedCredentials[0];
        Password = parsedCredentials[1];
    }

    protected abstract string[] Parse(string? credentials);
}
