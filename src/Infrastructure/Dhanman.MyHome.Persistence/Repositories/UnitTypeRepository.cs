using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.UnitTypes;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Persistence.Repositories;

internal class UnitTypeRepository: IUnitTypeRepository
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Contructor
    public UnitTypeRepository(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public DbSet<UnitType> UnitType => _dbContext.SetInt<UnitType>();
    public async Task<UnitType?> GetBydIdIntAsync(int id) => await _dbContext.GetBydIdIntAsync<UnitType>(id);

    public async Task<int> GetBydNameAsync(string unitType)
    {
        var unitTypeEntity = await _dbContext.SetInt<UnitType>()
                                            .FirstOrDefaultAsync(x => x.Name == unitType);

        return unitTypeEntity.Id;
    }
    public void Insert(UnitType unitType) => _dbContext.InsertInt(unitType);
    public void Delete(UnitType unitType) => _dbContext.RemoveInt(unitType);
    public void Update(UnitType unitType) => _dbContext?.UpdateInt(unitType);
    public int GetTotalRecordsCount() => UnitType.Count();

    #endregion
}
