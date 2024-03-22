using Dhanman.Infrastructure.Authentication.Constants;
using Dhanman.MyHome.Application.Abstractions.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Dhanman.Infrastructure.Authentication.Providers;

internal sealed class UserIdentifierProvider : IUserIdentifierProvider
{
    public UserIdentifierProvider(IHttpContextAccessor httpContextAccessor)
    {
        string userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirstValue(DhanmanJwtClaimTypes.UserId)
            ?? throw new ArgumentException("The user identifier claim is required.", nameof(httpContextAccessor));

        UserId = new Guid(userIdClaim);
    }

    public Guid UserId { get; }
}