using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.VisitorLogs;

public class VisitorLog : EntityInt, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties
    public int VisitorId { get; set; }
    public int? VisitorTypeId { get; set; }
    public string VisitingFrom { get; set; }
    //checked-in, checked-out, pending approval
    public DateTime EntryTime { get; set; }
    public DateTime? ExitTime { get; set; }
    public int VisitorStatusId { get; set; }
    #endregion

    #region Audit Properties
    public DateTime CreatedOnUtc { get; }

    public DateTime? ModifiedOnUtc { get; }

    public DateTime? DeletedOnUtc { get; }

    public bool IsDeleted { get; }

    public Guid CreatedBy { get; }

    public Guid? ModifiedBy { get; }
    #endregion

    #region Constructor
    public VisitorLog(int visitorId, int? visitorTypeId, string visitingFrom, DateTime entryTime, DateTime? exitTime, int visitorStatusId)
    {
        VisitorId = visitorId;
        VisitorTypeId = visitorTypeId;
        VisitingFrom = visitingFrom;
        EntryTime = entryTime;
        ExitTime = exitTime;
        VisitorStatusId = visitorStatusId;
    }
    #endregion
}
