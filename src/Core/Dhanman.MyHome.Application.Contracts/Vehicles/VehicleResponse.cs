namespace Dhanman.MyHome.Application.Contracts.Vehicles;

public sealed class VehicleResponse
{
    #region Properties 
    public int Id { get; }
    public string VehicleNumber { get; }
    public int VehicleTypeId { get; }
    public string VehicleType { get; }
    public int UnitId { get; }
    public string Unit { get; }  // Flat 
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; }
    public Guid CreatedBy { get; }
    public Guid? ModifiedBy { get; }

    #endregion

    #region Constructor
    public VehicleResponse(int id, string vehicleNumber, int vehicleTypeId, string vehicleType, int unitId, string unit, DateTime createdOnUtc, DateTime? modifiedOnUtc, Guid createdBy, Guid? modifiedBy)
    {
        Id = id;
        VehicleNumber = vehicleNumber;
        VehicleTypeId = vehicleTypeId;
        VehicleType = vehicleType;
        UnitId = unitId;
        Unit = unit;
        CreatedOnUtc = createdOnUtc;
        ModifiedOnUtc = modifiedOnUtc;
        CreatedBy = createdBy;
        ModifiedBy = modifiedBy;
    }

    #endregion
}