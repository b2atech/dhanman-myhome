using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.VisitorLogs.Commands.RejectVisitorLog;

public class RejectVisitorLogCommand : ICommand<Result<EntityUpdatedResponse>>
{
    #region Properties
    public int VisitorLogId { get; set; }
    #endregion

    #region Constructor
    public RejectVisitorLogCommand(int visitorLogId)
    {
        VisitorLogId = visitorLogId;
    }

    #endregion

}
