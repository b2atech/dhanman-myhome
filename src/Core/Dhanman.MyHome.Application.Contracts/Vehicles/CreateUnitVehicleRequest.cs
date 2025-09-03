namespace Dhanman.MyHome.Application.Contracts.Vehicles;

public class CreateUnitVehicleRequest
{
    public string VehicleNumber { get; set; }
    public int VehicleTypeId { get; set; }
    public int UnitId { get; set; }
    public string? VehicleRfId { get; set; }
    public string? VehicleRfIdSecretcode { get; set; }
    public Guid CreatedBy { get; set; }
}
