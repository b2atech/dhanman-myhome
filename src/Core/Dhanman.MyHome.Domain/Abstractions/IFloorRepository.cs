using Dhanman.MyHome.Domain.Entities.Floors;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface IFloorRepository
{
    #region Methods
    Task<Floor?> GetByIntIdAsync(int id);
    void Insert(Floor floor);
    void Update(Floor floor);
    void Delete(Floor floor);
   // Task<int> GetLastFloorIdAsync();

    #endregion
}
