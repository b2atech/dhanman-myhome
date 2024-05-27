using Dhanman.MyHome.Domain.Entities.Buildings;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface IBuildingRepository
{
    #region Methods

    Task<Building?> GetBydIdIntAsync(int id);

    void Insert(Building building);
    Task<int> GetLastBuildingIdAsync();

    #endregion
}
