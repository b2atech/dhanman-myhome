using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.ServiceProviders;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Persistence.Repositories;

internal sealed class ServiceProviderRepository : IServiceProviderRepository
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Contructor
    public ServiceProviderRepository(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public DbSet<ServiceProvider> ServiceProvider => _dbContext.SetInt<ServiceProvider>();
    public async Task<ServiceProvider?> GetByIdIntAsync(int id) => await _dbContext.GetByIdIntAsync<ServiceProvider>(id);

    public void Insert(ServiceProvider serviceProvider) => _dbContext.InsertInt(serviceProvider);
    public void Delete(ServiceProvider serviceProvider) => _dbContext.RemoveInt(serviceProvider);
    public void Update(ServiceProvider serviceProvider) => _dbContext?.UpdateInt(serviceProvider);
    public int GetTotalRecordsCount() => ServiceProvider.Count();

    #endregion
}