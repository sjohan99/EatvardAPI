using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Domain.Utils;
using Domain.Utils.CredentialsParsing;
using Domain.Utils.Security;
using EatvardDataAccessLibrary;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace EatvardAPI.Handlers;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IUnitOfWork _unitOfWork;

    public BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IUnitOfWork unitOfWork)
        : base(options, logger, encoder, clock)
    {
        _unitOfWork = unitOfWork;
    }
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization")) 
        {
            return AuthenticateResult.Fail("No authorization header found");
        }

        try {
            CredentialsParser credentials = getCredentials();
            var email = credentials.Identity;
            var password = credentials.Password;
            var passwordHasher = PasswordHasherFactory.SHA256();

            var user = _unitOfWork.Users.Find(user => user.Email == email).FirstOrDefault();
            if (user == null) {
                return AuthenticateResult.Fail($"No user with email {email} found");
            }

            var hashedPassword = passwordHasher.Hash(password, user.PasswordSalt);
            if (user.PasswordHash != hashedPassword) {
                return AuthenticateResult.Fail("Invalid email or password");
            }

            var ticket = getAuthenticationTicket(user);

            return AuthenticateResult.Success(ticket);
        }
        catch (Exception ex) 
        {
            return AuthenticateResult.Fail("Error has occured");
        };

    }

    private CredentialsParser getCredentials()
    {
        var authenticationHeaderValue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
        return new Base64CredentialsParser(authenticationHeaderValue.Parameter);
    }

    private AuthenticationTicket getAuthenticationTicket(EatvardDataAccessLibrary.Models.User user)
    {
        var claims = new[] { new Claim(ClaimTypes.Name, user.Email) };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);
        return ticket;
    }
}
