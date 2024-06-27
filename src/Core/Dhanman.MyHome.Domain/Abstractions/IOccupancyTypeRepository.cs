using Dhanman.MyHome.Domain.Entities.OccupancyTypes;
using Dhanman.MyHome.Domain.Entities.OccupantTypes;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface IOccupancyTypeRepository
{
    #region Methods
    Task<OccupancyType?> GetByIdIntAsync(int id);
    Task<int> GetBydNameAsync(string occupancyType);
    void Insert(OccupantType occupancyType);

    void Delete(OccupantType occupancyType);

    void Update(OccupancyType occupancyType);

    int GetTotalRecordsCount();

    #endregion
}
