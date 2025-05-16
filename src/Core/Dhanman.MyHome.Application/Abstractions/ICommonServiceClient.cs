using Dhanman.MyHome.Application.Contracts;
using Dhanman.MyHome.Application.Contracts.Users;

namespace Dhanman.MyHome.Application.Abstractions;
 
public interface ICommonServiceClient
{
    Task<string> CreateCustomerAsync(CustomerDto customer); 
    Task<string> CreateUserAsync(UserDto user);
    Task<UserIdResponse?> GetUserByEmailOrPhoneAsync(string? email, string? phoneNo);
}
