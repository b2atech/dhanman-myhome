using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Units;

namespace Dhanman.MyHome.Application.Features.Units.Command.GetUnitDetails;

public class GetUnitByFloorIdCommand : ICommand<Result<UnitByFloorIdListResponse>>
{
    public List<int> BuildingIds { get; set; }
    public List<int> FloorIds { get; set; }
    public GetUnitByFloorIdCommand(List<int> buildingIds, List<int> floorIds)
    {
        BuildingIds = buildingIds;
        FloorIds = floorIds;
    }
}
