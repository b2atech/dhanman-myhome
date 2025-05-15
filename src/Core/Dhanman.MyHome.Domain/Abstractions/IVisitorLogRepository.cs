using Dhanman.MyHome.Domain.Entities.VisitorLogs;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface IVisitorLogRepository
{
    #region Methods
    Task<VisitorLog?> GetByIntIdAsync(int id);
    void Insert(VisitorLog visitorLog);
    void Update(VisitorLog visitorLog);
    void Delete(VisitorLog visitorLog);
    //int GetTotalRecordsCount();
    #endregion
}