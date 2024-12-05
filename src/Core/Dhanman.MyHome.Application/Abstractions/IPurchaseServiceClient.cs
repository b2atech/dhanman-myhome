using Dhanman.MyHome.Application.Contracts;

namespace Dhanman.MyHome.Application.Abstractions;

public interface IPurchaseServiceClient
{
    Task<string> CreateUserAsync(UserDto user);
}
