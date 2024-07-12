using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.VisitorLogs;

public class VisitorLog : EntityInt, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties
    public int VisitorId { get; set; }
    public int VisitingUnitId { get; set; }
    public int? VisitorTypeId { get; set; }
    public string VisitingFrom { get; set; }
    //checked-in, checked-out, pending approval
    public int CurrentStatusId { get; set; }
    public DateTime EntryTime { get; set; }
    public DateTime? ExitTime { get; set; }

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
    public VisitorLog(int id, int visitorId, int visitingUnitId, int? visitorTypeId, string visitingFrom, int currentStatusId, DateTime entryTime, DateTime? exitTime)
    {
        Id = id;
        VisitorId = visitorId;
        VisitingUnitId = visitingUnitId;
        VisitorTypeId = visitorTypeId;
        VisitingFrom = visitingFrom;
        CurrentStatusId = currentStatusId;
        EntryTime = entryTime;
        ExitTime = exitTime;
    }
    #endregion
}
