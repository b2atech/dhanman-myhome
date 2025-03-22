using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.VisitorsApproved;

public class VisitorsApproved : EntityInt, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties
    public int VisitorId { get; set; } 
    public int VisitTypeId { get; set; }  // Either 'single' or 'recurring'
    public DateTime? StartDate { get; set; }  
    public DateTime? EndDate { get; set; }
    public TimeSpan? EntryTime { get; set; } 
    public TimeSpan? ExitTime { get; set; }
    #endregion

    #region Audit Properties
    public DateTime CreatedOnUtc { get; set; }  
    public DateTime? ModifiedOnUtc { get; set; } 
    public Guid CreatedBy { get; set; }  // Who created the approval entry (flat owner)
    public Guid? ModifiedBy { get; set; }  
    public DateTime? DeletedOnUtc { get; }
    public bool IsDeleted { get; set; }
    #endregion

}
