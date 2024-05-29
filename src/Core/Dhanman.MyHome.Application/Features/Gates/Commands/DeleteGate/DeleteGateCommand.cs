using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;

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
