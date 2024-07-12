using Dhanman.MyHome.Domain.Entities.VisitorLogs;
using Dhanman.MyHome.Domain.Entities.Visitors;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface IVisitorLogRepository
{
    #region Methods
    Task<VisitorLog?> GetByIntIdAsync(int id);
    void Insert(VisitorLog visitorLog);
    void Update(VisitorLog visitorLog);
    void Delete(VisitorLog visitorLog);  
    #endregion
}