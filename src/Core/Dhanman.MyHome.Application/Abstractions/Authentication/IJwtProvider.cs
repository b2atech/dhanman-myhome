using Dhanman.MyHome.Application.Contracts.Authentication;
using Dhanman.MyHome.Domain.Entities.Users;

namespace Dhanman.MyHome.Application.Abstractions.Authentication;

public interface IJwtProvider
{
    Task<TokenResponse> CreateAsync(User user);
}
