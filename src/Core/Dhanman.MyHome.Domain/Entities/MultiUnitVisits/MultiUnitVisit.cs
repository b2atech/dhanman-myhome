using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.MultiUnitVisits;

public class MultiUnitVisit : EntityLong, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties
    public long Id { get; set; }
    public int VisitorLogId { get; set; }
    public int UnitId { get; set; }
    public int Visitor_Statuses{ get; set; }
    #endregion

    #region Auditable Properties
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
    public DateTime? DeletedOnUtc { get; set; }
    public bool IsDeleted { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid? ModifiedBy { get; set; }
    #endregion

    #region Constructor
    public MultiUnitVisit(long id, int visitorLogId, int unitId, int visitor_Statuses)
    {
        Id = id;
        VisitorLogId = visitorLogId;
        UnitId = unitId;
        Visitor_Statuses = visitor_Statuses;
    }
    #endregion
}