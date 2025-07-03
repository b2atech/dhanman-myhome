using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.UnitServiceProviders;

namespace Dhanman.MyHome.Application.Features.UnitServiceProviders.Commands.GetAssignUnits;

public class GetAssignUnitsCommand : ICommand<Result<AssignSPUnitsListResponse>>
{
    public List<int> BuildingIds { get; set; }
    public GetAssignUnitsCommand(List<int> buildingIds)
    {
        BuildingIds = buildingIds;
    }
}
