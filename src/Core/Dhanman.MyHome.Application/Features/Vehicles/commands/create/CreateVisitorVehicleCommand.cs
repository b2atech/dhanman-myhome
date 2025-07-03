using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.Vehicles.commands.create;

public class CreateVisitorVehicleCommand : ICommand<Result<EntityCreatedResponse>>
{
    #region Properties
    public int VisitorVehicleId { get; set; }
    public int VisitorLogId { get; set; }
    public string? VehicleNumber { get; set; }
    public string? VehicleType { get; set; }

    #endregion

    #region Constructor
    public CreateVisitorVehicleCommand( int visitorLogId, string? vehicleNumber, string? vehicleType)
    {
        VisitorLogId = visitorLogId;
        VehicleNumber = vehicleNumber;
        VehicleType = vehicleType;
    }
    #endregion
}
