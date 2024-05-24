using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Units;

namespace Dhanman.MyHome.Application.Features.Units.Command.GetUnitDetails;

public class GetUnitDetailsCommand: ICommand<Result<UnitDetailListResponse>>
{
    public List<int> BuildingIds { get; set; }
    public List<int> OccupanyTypeIds { get; set; }
    public GetUnitDetailsCommand(List<int> buildingIds, List<int> occupanyTypeIds)
    {
         BuildingIds = buildingIds;
         OccupanyTypeIds = occupanyTypeIds;
    }
  
}


