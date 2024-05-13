using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.UnitServiceProviders;

public class UnitServiceProvider : EntityInt, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties
    public int Id { get; set; }
    public int UnitId { get; set; }
    public int ServiceProviderId { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; set; }
    public DateTime? DeletedOnUtc { get; }
    public bool IsDeleted { get; }
    public Guid CreatedBy { get; protected set; }
    public Guid? ModifiedBy { get; protected set; }
    #endregion

    #region Constructor

    public UnitServiceProvider(int id, int unitId, int serviceProviderId, DateTime start, DateTime end)
    {
        Id = id;
        UnitId = unitId;
        ServiceProviderId = serviceProviderId;
        Start = start;
        End = end;
    }
    #endregion

}
