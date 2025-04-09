using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.VisitorVehicles;

public class VisitorVehicles : EntityInt, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties
    public int VisitorLogId { get; set; }
    public string VehicleNumber { get; set; }
    public string VehicleType { get; set; } //e.g. Car, Bike, Traveler, Tanker
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
    public VisitorVehicles(int visitorLogId, string vehicleNumber, string vehicleType)
    {
        VisitorLogId = visitorLogId;
        VehicleNumber = vehicleNumber;
        VehicleType = vehicleType;
    }
    #endregion
}
