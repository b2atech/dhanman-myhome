using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.Vehicles.commands.CreateVehicle;

public class CreateVehicleCommand : ICommand<Result<EntityCreatedResponse>>
{
    public int VehicleId { get; set; }
    public string VehicleNumber { get; }
    public int VehicleTypeId { get; }
    public int UnitId { get; }
    public string? VehicleRfId { get; }
    public string? VehicleRfIdSecretcode { get; }
    public Guid CreatedBy { get; }

    public CreateVehicleCommand(
        string vehicleNumber,
        int vehicleTypeId,
        int unitId,
        string? vehicleRfId,
        string? vehicleRfIdSecretcode,
        Guid createdBy)
    {
        VehicleNumber = vehicleNumber;
        VehicleTypeId = vehicleTypeId;
        UnitId = unitId;
        VehicleRfId = vehicleRfId;
        VehicleRfIdSecretcode = vehicleRfIdSecretcode;
        CreatedBy = createdBy;
    }
}
