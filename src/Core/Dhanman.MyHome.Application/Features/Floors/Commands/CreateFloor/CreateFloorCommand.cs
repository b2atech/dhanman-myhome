using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.Floors.Commands.CreateFloor;

public class CreateFloorCommand : ICommand<Result<EntityCreatedResponse>>
{
    #region Properties
    public string Name { get; set; }
    public Guid ApartmentId { get; set; }
    public int BuildingId { get; set; }
    public int TotalUnits { get; set; }
    #endregion

    #region Constructors
    public CreateFloorCommand(string name, Guid apartmentId, int buildingId, int totalUnits)
    {
        Name = name;
        ApartmentId = apartmentId;
        BuildingId = buildingId;
        TotalUnits = totalUnits;
    }
    #endregion
}