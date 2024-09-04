using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Dhanman.MyHome.Api
{
    public static class Auth0Extensions
    {
        public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration, string dummy)
        {

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
                    ValidAudiences = new[] { configuration["Auth0:Audience"], configuration["Auth0:NativeAudience"] },
                    NameClaimType = ClaimTypes.NameIdentifier
                };
            });
        }
    }
}
