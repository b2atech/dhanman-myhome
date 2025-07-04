using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;

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
