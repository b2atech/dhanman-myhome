using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;

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
