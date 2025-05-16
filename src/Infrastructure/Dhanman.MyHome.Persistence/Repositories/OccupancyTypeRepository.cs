using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.OccupancyTypes;
using Dhanman.MyHome.Domain.Entities.OccupantTypes;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Persistence.Repositories;

internal class OccupancyTypeRepository : IOccupancyTypeRepository
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Contructor
    public OccupancyTypeRepository(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public DbSet<OccupancyType> OccupancyType => _dbContext.SetInt<OccupancyType>();
    public async Task<OccupancyType?> GetBydIdIntAsync(int id) => await _dbContext.GetBydIdIntAsync<OccupancyType>(id);

    public async Task<int> GetBydNameAsync(string occupancyType)
    {
        var occupancyTypeEntity = await _dbContext.SetInt<OccupancyType>()
                                            .FirstOrDefaultAsync(x => x.Name == occupancyType);

        return occupancyTypeEntity.Id;
    }

    public void Insert(OccupantType occupancyType) => _dbContext.InsertInt(occupancyType);
    public void Delete(OccupantType occupancyType) => _dbContext.RemoveInt(occupancyType);
    public void Update(OccupancyType occupancyType) => _dbContext?.UpdateInt(occupancyType);
    //public int GetTotalRecordsCount() => OccupancyType.Count();


    #endregion
}
