using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace EatvardAPI.JWT;

public class JWTUtils
{
    public readonly string _secretKey;
    private IConfiguration _configuration;

    public JWTUtils(IConfiguration configuration)
    {
        _configuration = configuration;
        _secretKey = configuration["Eatvard:JWTSettings"];
    }

    public string GenerateToken(string email)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);
        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.Name, email)
            }),
            Expires = DateTime.UtcNow.AddMonths(6),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
