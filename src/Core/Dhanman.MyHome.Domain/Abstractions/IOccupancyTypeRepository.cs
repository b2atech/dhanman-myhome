using Dhanman.MyHome.Domain.Entities.Apartments;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface IOccupancyTypeRepository
{
    #region Methods
    Task<OccupancyType?> GetBydIdIntAsync(int id);
    Task<int> GetBydNameAsync(string occupancyType);
    void Insert(OccupantType occupancyType);

    void Delete(OccupantType occupancyType);

    void Update(OccupancyType occupancyType);

    int GetTotalRecordsCount();

    #endregion
}
