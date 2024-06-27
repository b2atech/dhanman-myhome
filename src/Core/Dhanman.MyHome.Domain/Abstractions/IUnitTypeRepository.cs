using Dhanman.MyHome.Domain.Entities.UnitTypes;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface IUnitTypeRepository
{
    #region Methods
    Task<UnitType?> GetByIdIntAsync(int id);
    Task<int> GetBydNameAsync(string unitType);
    void Insert(UnitType unitType);

    void Delete(UnitType unitType);

    void Update(UnitType unitType);

    int GetTotalRecordsCount();

    #endregion
}
