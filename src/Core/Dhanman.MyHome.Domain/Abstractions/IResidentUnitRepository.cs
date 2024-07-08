using Dhanman.MyHome.Domain.Entities.ResidentUnits;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface IResidentUnitRepository
{
    #region Methods
    Task<ResidentUnit?> GetBydIdIntAsync(int id);

    void Insert(ResidentUnit residentUnit);

    void Delete(ResidentUnit residentUnit);

    void Update(ResidentUnit residentUnit);

    Task<int> GetLastResidentUnitIdAsync();
    int GetTotalRecordsCount();
    List<ResidentUnit> GetByResidentId(int residentId);
    #endregion

}
