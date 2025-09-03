namespace Dhanman.MyHome.Application.Contracts.Vehicles;

public class BasicVehicleInfoResponse
{
    public int Id { get; }
    public string VehicleNumber { get; }
    public int VehicleTypeId { get; }
    public int UnitId { get; }
    public string VehicleRfId { get; }
    public string VehicleRfIdSecretcode { get; }
    public DateTime CreatedOnUtc { get; }

    public BasicVehicleInfoResponse(
        int id,
        string vehicleNumber,
        int vehicleTypeId,
        int unitId,
        string vehicleRfId,
        string vehicleRfIdSecretcode,
        DateTime createdOnUtc)
    {
        Id = id;
        VehicleNumber = vehicleNumber;
        VehicleTypeId = vehicleTypeId;
        UnitId = unitId;
        VehicleRfId = vehicleRfId;
        VehicleRfIdSecretcode = vehicleRfIdSecretcode;
        CreatedOnUtc = createdOnUtc;
    }
}
