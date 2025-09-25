using Dhanman.MyHome.Application.Contracts.Enums;
using Dhanman.MyHome.Application.Contracts.VisitorApprovals;
using Dhanman.MyHome.Domain.Entities.VisitorApprovals;
using Dhanman.MyHome.Domain.Entities.Visitors;


namespace Dhanman.MyHome.Domain.Abstractions;

public interface IVisitorRepository
{
    #region Methods
    Task<Visitor?> GetByIntIdAsync(int id);
    void Insert(Visitor visitor);
    void Update(Visitor visitor);
    void Delete(Visitor visitor);
    /// <summary>
    /// Approves a visitor for the given unit and updates overall visitor log status.
    /// Returns approver details and final status.
    /// </summary>
    Task<VisitorApprovalActionResponse?> TakeVisitorActionAsync(
       int visitorLogId,
       int unitId,
       Guid residentUserId,
       VisitorStatus action,
       CancellationToken cancellationToken = default);
    #endregion

}
