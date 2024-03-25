using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.Apartments;

public class Vehicle: EntityInt, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties
    public string VehicleNumber { get; set; }
    public int VehicleTypeId { get; set; }
    public int UnitId { get; set; }
    public string? VehicleRfid  { get; set; }
    public string? VehicleRfidSecretCode { get; set; }
    #endregion

    #region Audit Properties
    public DateTime CreatedOnUtc { get; }

    public DateTime? ModifiedOnUtc { get; }

    public DateTime? DeletedOnUtc { get; }

    public bool IsDeleted { get; }

    public Guid CreatedBy { get; protected set; }

    public Guid? ModifiedBy { get; protected set; }
    #endregion

    #region Constructor
    public Vehicle(int id, string vehicleNumber, int vehicleTypeId, int unitId, string? vehicleRfid, string? vehicleRfidSecretCode, Guid createdBy)
    {
        Id = id; 
        VehicleNumber = vehicleNumber;
        VehicleTypeId = vehicleTypeId;
        UnitId = unitId;
        VehicleRfid = vehicleRfid;
        VehicleRfidSecretCode = vehicleRfidSecretCode;
        CreatedBy = createdBy;

    }
    #endregion
}
