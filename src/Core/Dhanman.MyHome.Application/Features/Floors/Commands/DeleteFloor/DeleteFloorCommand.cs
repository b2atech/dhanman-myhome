using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.Floors.Commands.DeleteFloor;

public class DeleteFloorCommand : ICommand<Result<EntityDeletedResponse>>
{
    #region Properties
    public int FloorId { get; }

    #endregion

    #region Constructors
    public DeleteFloorCommand(int floorId)
    {
        FloorId = floorId;
    }
    #endregion
}
