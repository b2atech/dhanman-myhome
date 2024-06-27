using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Dhanman.Infrastructure.Authentication;

public static class DependencyInjection
{
    public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration,string dummy)
    {

        #region Methodes
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.Authority = $"https://{configuration["Auth0:Domain"]}/";
            options.Audience = configuration["Auth0:Audience"];
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = $"https://{configuration["Auth0:Domain"]}/",
                ValidAudience = configuration["Auth0:Audience"],
                NameClaimType = ClaimTypes.NameIdentifier
            };
        });

        #endregion
    }
}
