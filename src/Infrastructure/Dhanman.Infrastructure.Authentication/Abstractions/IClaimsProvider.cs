using Dhanman.MyHome.Domain.Entities.Users;
using System.Security.Claims;

namespace Dhanman.Infrastructure.Authentication.Abstractions;

internal interface IClaimsProvider
{
    Task<Claim[]> GetClaimsAsync(User user);
}
