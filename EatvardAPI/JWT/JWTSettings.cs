namespace EatvardAPI.JWT;

public class JWTSettings
{
    public readonly string SecretKey;
    private IConfiguration _configuration;

    public JWTSettings(IConfiguration configuration)
    {
        _configuration = configuration;
        SecretKey = configuration["Eatvard:JWTSettings"];
    }
}
