using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Dhanman.MyHome.Api;

public static class Auth0Extensions
{
    public static IServiceCollection AddAuth0Authentication(this IServiceCollection services, IConfiguration configuration)
    {
        var domain = $"https://{configuration["Auth0:Domain"]}/";
        var audience = configuration["Auth0:Audience"];

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.Authority = domain;
            options.Audience = audience;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = domain,
                ValidateAudience = true,
                ValidAudience = audience,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true
            };
        });

        return services;
    }
}
