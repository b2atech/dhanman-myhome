using Dhanman.MyHome.Application.Contracts;

namespace Dhanman.MyHome.Application.Abstractions;
 
public interface ICommonServiceClient
{
    Task<string> CreateCustomerAsync(CustomerDto customer); 
}
