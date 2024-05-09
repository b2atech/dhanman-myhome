using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.UnitVehicleLimits;

public class UnitVehicleLimit : EntityInt, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties
    public int UnitId { get; set; }
    public int CarLimit { get; set; }
    public int TwoWheelarsLimit { get; set; }
    public int NoOfCarsAllotted { get; set; }
    public int NoOfTwoWheelarsAllotted { get; set; }
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
    public UnitVehicleLimit(int id, int unitId, int carLimit, int twoWheelarsLimit, int noOfCarsAllotted, int noOfTwoWheelarsAllotted, Guid createdBy)
    {
        Id = id;
        UnitId = unitId;
        CarLimit = carLimit;
        TwoWheelarsLimit = twoWheelarsLimit;
        NoOfCarsAllotted = noOfCarsAllotted;
        NoOfTwoWheelarsAllotted = noOfTwoWheelarsAllotted;
        CreatedBy = createdBy;
        CreatedOnUtc = DateTime.UtcNow;
    }
    #endregion
}
