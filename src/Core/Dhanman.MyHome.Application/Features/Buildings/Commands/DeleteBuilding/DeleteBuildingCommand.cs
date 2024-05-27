using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;

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
