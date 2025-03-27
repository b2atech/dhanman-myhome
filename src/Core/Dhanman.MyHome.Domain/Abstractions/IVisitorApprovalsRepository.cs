using Dhanman.MyHome.Domain.Entities.VisitorApprovals;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface IVisitorApprovalsRepository
{
    #region Methods
    Task<VisitorApproval> GetBydIdIntAsync(int id);

    void Insert(VisitorApproval visitorApproval);

    void Update(VisitorApproval visitorApproval);

    void Delete(VisitorApproval visitorApproval);
    #endregion
}
