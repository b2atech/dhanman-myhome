using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Buildings;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Persistence.Repositories;

public class BuildingRepository : IBuildingRepository
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Contructor
    public BuildingRepository(IApplicationDbContext dbContext) => _dbContext = dbContext;

    #endregion

    #region Methods

    public void Insert(Building building) => _dbContext.InsertInt(building);

    Task<Building?> IBuildingRepository.GetByIntIdAsync(int id) => _dbContext.GetBydIdIntAsync<Building>(id);
    public async Task<int> GetLastBuildingIdAsync()
    {
        return await _dbContext.SetInt<Building>()
             .IgnoreQueryFilters()
            .OrderByDescending(b => b.Id)
            .Select(b => b.Id)
            .FirstOrDefaultAsync();
    }

    public void Update(Building building) => _dbContext.UpdateInt(building);

    public void Delete(Building building) => _dbContext.RemoveInt(building);

    #endregion
}
