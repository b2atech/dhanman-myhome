using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Floors;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Persistence.Repositories;

public class FloorRepository : IFloorRepository
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Contructor
    public FloorRepository(IApplicationDbContext dbContext) => _dbContext = dbContext;

    #endregion

    #region Methods

    public Task<Floor?> GetByIntIdAsync(int id) => _dbContext.GetBydIdIntAsync<Floor>(id);

    public async Task<int> GetLastFloorIdAsync()
    {
        return await _dbContext.SetInt<Floor>()
            .IgnoreQueryFilters()
            .OrderByDescending(b => b.Id)
            .Select(b => b.Id)
            .FirstOrDefaultAsync();
    }

    public void Insert(Floor floor) => _dbContext.InsertInt(floor);

    public void Update(Floor floor) => _dbContext.UpdateInt(floor);
    public void Delete(Floor floor)
    {
        throw new NotImplementedException();
    }
    #endregion
}
