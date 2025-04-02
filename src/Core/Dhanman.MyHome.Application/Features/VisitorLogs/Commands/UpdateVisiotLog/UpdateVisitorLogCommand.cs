using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.VisitorLogs.Commands.UpdateVisiotLog;

public class UpdateVisitorLogCommand : ICommand<Result<EntityUpdatedResponse>>
{
    #region Properties
    public int VisitorLogId { get; set; }
     #endregion

    #region Constructor
    public UpdateVisitorLogCommand(int visitorLogId)
    {
        VisitorLogId = visitorLogId;
    }

    #endregion

}
