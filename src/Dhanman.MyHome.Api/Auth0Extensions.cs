using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Dhanman.MyHome.Api
{
    public static class Auth0Extensions
    {
        /// <summary>
        /// Registers authentication with Auth0 by default, or local JWT validation if envName == "test".
        /// </summary>
        public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration, string envName)
        {
            if (envName?.ToLowerInvariant() == "test")


            {
                // Use locally signed JWT for test environment
                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = configuration["Jwt:Authority"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SigningKey"])),
                        ValidateIssuerSigningKey = true,
                        NameClaimType = ClaimTypes.NameIdentifier
                    };
                });
            }
            else
            {
                // Default: Use Auth0 as before
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
                        ValidAudiences = new[]
                        {
                            configuration["Auth0:Audience"],
                            configuration["Auth0:NativeAudience"]
                        },
                        NameClaimType = ClaimTypes.NameIdentifier
                    };
                });
            }

        }
    }
}