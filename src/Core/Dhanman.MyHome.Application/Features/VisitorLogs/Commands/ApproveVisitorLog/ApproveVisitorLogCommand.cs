using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.VisitorLogs.Commands.ApproveVisitorLog;

public class ApproveVisitorLogCommand : ICommand<Result<EntityUpdatedResponse>>
{
    #region Properties
    public int VisitorLogId { get; set; }
    #endregion

    #region Constructor
    public ApproveVisitorLogCommand(int visitorLogId)
    {
        VisitorLogId = visitorLogId;
    }

    #endregion

}
