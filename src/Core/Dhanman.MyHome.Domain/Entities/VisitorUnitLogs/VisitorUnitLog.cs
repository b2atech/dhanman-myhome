using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.VisitorUnitLogs;
public class VisitorUnitLog : EntityInt, ISoftDeletableEntity
{
    #region Properties
    public int VisitorLogId { get; set; }
    public int UnitId { get; set; }        
    public DateTime? DeletedOnUtc { get; }
    public bool IsDeleted { get; }
    #endregion

    #region Constructor
    public VisitorUnitLog(int id, int visitorLogId, int unitId)
    {
        Id = id;
        VisitorLogId = visitorLogId;
        UnitId = unitId;
    }
    #endregion
}