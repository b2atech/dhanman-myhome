using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Apartments;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Persistence.Repositories;

internal class OccupantTypeRepository : IOccupantTypeRepository
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Contructor
    public OccupantTypeRepository(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public DbSet<OccupantType> OccupantType => _dbContext.SetInt<OccupantType>();
    public async Task<OccupantType?> GetBydIdIntAsync(int id) => await _dbContext.GetBydIdIntAsync<OccupantType>(id);

    public async Task<int> GetBydNameAsync(string occupantType)
    {
        var occupantTypeEntity = await _dbContext.SetInt<OccupantType>()
                                            .FirstOrDefaultAsync(x => x.Name == occupantType);

        return occupantTypeEntity.Id;
    }
    public void Insert(OccupantType occupantType) => _dbContext.InsertInt(occupantType);
    public void Delete(OccupantType occupantType) => _dbContext.RemoveInt(occupantType);
    public void Update(OccupantType occupantType) => _dbContext?.UpdateInt(occupantType);
    public int GetTotalRecordsCount() => OccupantType.Count();

    #endregion
}
