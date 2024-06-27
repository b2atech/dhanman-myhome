using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.UnitServiceProviders;


namespace Dhanman.MyHome.Persistence.Repositories;

internal sealed class UnitServiceProviderRespository: IUnitServiceProviderRespository
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Contructor
    public UnitServiceProviderRespository(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<UnitServiceProvider?> GetByIdIntAsync(int id) => await _dbContext.GetByIdIntAsync<UnitServiceProvider>(id);

    public void InsertInt(UnitServiceProvider unitServiceProvider) => _dbContext.InsertInt(unitServiceProvider);

    public void DeleteInt(UnitServiceProvider unitServiceProvider) => _dbContext.RemoveInt(unitServiceProvider);

    public void UpdateInt(UnitServiceProvider updateUnitServiceProvider) => _dbContext?.UpdateInt(updateUnitServiceProvider);
    #endregion
}
