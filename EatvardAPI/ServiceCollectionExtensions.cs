using System.Text;
using Domain.Repositories;
using EatvardAPI.JWT;
using EatvardDataAccessLibrary;
using EatvardDataAccessLibrary.Repositories;
using EatvardDataAccessLibrary.Repositories.RestaurantRepository;
using EatvardDataAccessLibrary.Repositories.UserRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EatvardAPI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddJWT(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<JWTUtils>();

        var jwtKey = Encoding.ASCII.GetBytes(configuration["Eatvard:JWTSettings"]);
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options => {
            options.RequireHttpsMetadata = true;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(jwtKey),
                ValidateIssuer = false,
                ValidateAudience = false,
            };
        });
        
        return services;
    }

    public static IServiceCollection AddEFCoreDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IRestaurantRepository, RestaurantRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddDbContext<EatvardContext>(options =>
        {
            // UseLazyLoadingProxies() in order for nested entities to be populated when querying.
            options.UseLazyLoadingProxies().UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        return services;
    }
}
