using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Dhanman.Infrastructure.Authentication.Constants;

internal class DhanmanJwtDefaults
{
    internal const string AuthenticationScheme = JwtBearerDefaults.AuthenticationScheme;

    internal const string IssuerSettingsKey = "Jwt:Issuer";

    internal const string AudienceSettingsKey = "Jwt:Audience";

    internal const string SecurityKeySettingsKey = "Jwt:SecurityKey";
}
