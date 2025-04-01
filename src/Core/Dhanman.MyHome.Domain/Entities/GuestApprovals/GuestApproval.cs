using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.GuestApprovals;

public class GuestApproval : EntityLong, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties
    public long VisitorLogId { get; set; } 
    public Guid? ApprovedBy { get; set; }
    public int? VisitorStatusId { get; set; }
    #endregion

    #region Audit Properties
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
    public DateTime? DeletedOnUtc { get; set; }
    public bool IsDeleted { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid? ModifiedBy { get; set; }
    #endregion

    #region Constructor
    public GuestApproval(long id,long visitorLogId, Guid? approvedBy, int? visitorStatusId)
    {
        Id = id;
        VisitorLogId = visitorLogId;
        ApprovedBy = approvedBy;
        VisitorStatusId = visitorStatusId;
    }
    #endregion
}
