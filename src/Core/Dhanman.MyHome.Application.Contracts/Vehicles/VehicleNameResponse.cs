namespace Dhanman.MyHome.Application.Contracts.Vehicles;

public sealed class VehicleNameResponse
{
    #region Properties 
    public int Id { get; }
    public string VehicleNumber { get; }
    #endregion

    #region Constructor
    public VehicleNameResponse(int id, string vehicleNumber)
    {
        Id = id;
        VehicleNumber = vehicleNumber;        
    }

    #endregion
}