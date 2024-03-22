using Dhanman.Infrastructure.Authentication.Abstractions;
using Dhanman.Infrastructure.Authentication.Options;
using Dhanman.MyHome.Application.Abstractions.Authentication;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Contracts.Authentication;
using Dhanman.MyHome.Domain.Entities.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Dhanman.Infrastructure.Authentication.Providers;

internal sealed class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _jwtOptions;
    private readonly IClaimsProvider _claimsProvider;
    private readonly IDateTime _dateTime;

    public JwtProvider(
        IOptions<JwtOptions> jwtOptions,
        IClaimsProvider claimsProvider,
        IDateTime dateTime)
    {
        _claimsProvider = claimsProvider;
        _dateTime = dateTime;
        _jwtOptions = jwtOptions.Value;
    }

    public async Task<TokenResponse> CreateAsync(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecurityKey));

        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        Claim[] claims = await _claimsProvider.GetClaimsAsync(user);

        DateTime tokenExpirationTime = _dateTime.UtcNow.AddMinutes(_jwtOptions.TokenExpirationInMinutes);

        var token = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            null,
            tokenExpirationTime,
            signingCredentials);

        string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return new TokenResponse(tokenValue);
    }
}
