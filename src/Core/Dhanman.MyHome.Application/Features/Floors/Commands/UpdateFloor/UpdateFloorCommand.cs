using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.Floors.Commands.UpdateFloor;

public class UpdateFloorCommand : ICommand<Result<EntityUpdatedResponse>>
{
    #region Properties
    public int FloorId { get; set; }
    public string Name { get; set; }
    public int BuildingId { get; set; }
    public int TotalUnits { get; set; }
    #endregion

    #region Constructors
    public UpdateFloorCommand(int floorId, string name, int buildingId, int totalUnits)
    {
        FloorId = floorId;
        Name = name;
        BuildingId = buildingId;
        TotalUnits = totalUnits;
    }
    #endregion
}
