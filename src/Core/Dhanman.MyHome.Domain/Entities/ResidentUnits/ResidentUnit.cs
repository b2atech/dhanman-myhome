using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.ResidentUnits;

public class ResidentUnit:EntityInt, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties
    public int Id { get; set; }
    public int UnitId { get; set; }
    public int ResidentId { get; set; }
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; set; }
    public DateTime? DeletedOnUtc { get; }
    public bool IsDeleted { get; }
    public Guid CreatedBy { get; protected set; }
    public Guid? ModifiedBy { get; protected set; }
    #endregion

    #region Constructor

    public ResidentUnit(int id, int unitId, int residentId)
    {
        Id = id;
        UnitId = unitId;
        ResidentId = residentId;
    }
    #endregion
}
