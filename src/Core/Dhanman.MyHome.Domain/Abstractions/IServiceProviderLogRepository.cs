using Dhanman.MyHome.Domain.Entities.ServiceProviderLogs;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface IServiceProviderLogRepository
{
    #region Methods
    Task<ServiceProviderLog?> GetByIdAsync(int id);

    void Insert(ServiceProviderLog serviceProviderLog);

    void Delete(ServiceProviderLog serviceProviderLog);

    void Update(ServiceProviderLog serviceProviderLog);

    #endregion
}