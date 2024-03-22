using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Contracts.Authentication;
using Dhanman.MyHome.Domain.Entities.Users;

namespace Dhanman.MyHome.Application.Abstractions.Authentication;

public interface IAuthenticationService
{
    Task<Result<TokenResponse>> RegisterAsync(string firstName, string lastName, Email email, Password password);

    Task<Result<TokenResponse>> LoginAsync(string email, string password);
}
