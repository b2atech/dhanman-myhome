using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.Buildings.Commands.DeleteBuilding;

public class DeleteBuildingCommand : ICommand<Result<EntityDeletedResponse>> 
{
    #region Properties
    public int BuildingId { get; }

    #endregion

    #region Constructors
    public DeleteBuildingCommand(int buildingId)
    {
        BuildingId = buildingId;
    }
    #endregion
}
