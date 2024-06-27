using Dhanman.MyHome.Domain.Entities.ServiceProviders;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface IServiceProviderRepository
{
    #region Methods
    Task<ServiceProvider?> GetByIdIntAsync(int id);

    void Insert(ServiceProvider serviceProvider);

    void Delete(ServiceProvider serviceProvider);

    void Update(ServiceProvider serviceProvider);

    int GetTotalRecordsCount();

    #endregion

}