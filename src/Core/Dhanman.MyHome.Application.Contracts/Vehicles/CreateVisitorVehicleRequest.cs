namespace Dhanman.MyHome.Application.Contracts.Vehicles;

public class CreateVisitorVehicleRequest
{
    public int VisitorLogId { get; set; }
    public string? VehicleNumber { get; set; }
    public string? VehicleType { get; set; }

}
