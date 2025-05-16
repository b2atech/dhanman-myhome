using Dhanman.MyHome.Domain.Entities.Buildings;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface IBuildingRepository
{
    #region Methods
    Task<Building?> GetByIntIdAsync(int id);

    void Insert(Building building);
    void Update(Building building);
    void Delete(Building building);
    //Task<int> GetLastBuildingIdAsync();
    #endregion
}
