using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.Gates.Commands.DeleteGate;

public class DeleteGateCommand : ICommand<Result<EntityDeletedResponse>>
{
    #region Properties
    public int GateId { get; }

    #endregion

    #region Constructors
    public DeleteGateCommand(int gateId)
    {
        GateId = gateId;
    }
    #endregion
}
