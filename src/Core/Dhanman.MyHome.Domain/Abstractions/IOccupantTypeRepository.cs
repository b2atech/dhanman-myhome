using Dhanman.MyHome.Domain.Entities.OccupantTypes;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface IOccupantTypeRepository
{
    #region Methods
    Task<OccupantType?> GetBydIdIntAsync(int id);
    Task<int> GetBydNameAsync(string occupantType);
    void Insert(OccupantType occupantType);

    void Delete(OccupantType occupantType);

    void Update(OccupantType occupantType);

    //int GetTotalRecordsCount();

    #endregion
}
