using Dhanman.MyHome.Application.Contracts;

namespace Dhanman.MyHome.Application.Abstractions;

public interface ISalesServiceClient
{
    Task<string> CreateCustomerAsync(CustomerDto customer);
}