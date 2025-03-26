using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.VisitorApprovals;

public class VisitorApproval : EntityInt, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties
    public int VisitorId { get; set; }
    public int VisitTypeId { get; set; }  // Either 'single' or 'recurring'
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public TimeOnly? EntryTime { get; set; }
    public TimeOnly? ExitTime { get; set; }
    #endregion

    #region Audit Properties
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
    public Guid CreatedBy { get; set; }  // Who created the approval entry (flat owner)
    public Guid? ModifiedBy { get; set; }
    public DateTime? DeletedOnUtc { get; }
    public bool IsDeleted { get; set; }

    public VisitorApproval(int visitorId, int visitTypeId, DateOnly? startDate, DateOnly? endDate, TimeOnly? entryTime, TimeOnly? exitTime)
    {
        VisitorId = visitorId;
        VisitTypeId = visitTypeId;
        StartDate = startDate;
        EndDate = endDate;
        EntryTime = entryTime;
        ExitTime = exitTime;
    }
    #endregion
}
