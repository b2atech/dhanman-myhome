using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.ServiceProviderLogs;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Persistence.Repositories;

internal sealed class ServiceProviderLogRepository : IServiceProviderLogRepository
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion


    #region Constructor
    public ServiceProviderLogRepository(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion


    #region Methods
    public DbSet<ServiceProviderLog> ServiceProviderLogs => _dbContext.SetInt<ServiceProviderLog>();


    public async Task<ServiceProviderLog?> GetByIdAsync(int id) => await _dbContext.GetBydIdIntAsync<ServiceProviderLog>(id);


    public void Insert(ServiceProviderLog serviceProviderLog) => _dbContext.InsertInt(serviceProviderLog);


    public void Delete(ServiceProviderLog serviceProviderLog) => _dbContext.RemoveInt(serviceProviderLog);


    public void Update(ServiceProviderLog serviceProviderLog) => _dbContext?.UpdateInt(serviceProviderLog);


    #endregion
}
