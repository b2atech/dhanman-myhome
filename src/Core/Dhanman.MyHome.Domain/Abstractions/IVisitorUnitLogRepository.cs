using Dhanman.MyHome.Domain.Entities.VisitorUnitLogs;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface IVisitorUnitLogRepository
{
    #region Methods
    Task<VisitorUnitLog?> GetByIntIdAsync(int id);
    void Insert(VisitorUnitLog visitorUnitLog);
    void Update(VisitorUnitLog visitorUnitLog);
    void Delete(VisitorUnitLog visitorUnitLog);

    int GetTotalRecordsCount();
    #endregion
}