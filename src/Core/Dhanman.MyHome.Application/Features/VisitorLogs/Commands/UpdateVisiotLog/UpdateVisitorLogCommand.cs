using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.VisitorLogs.Commands.UpdateVisiotLog;

public class UpdateVisitorLogCommand : ICommand<Result<EntityUpdatedResponse>>
{
    #region Properties
    public int VisitorLogId { get; set; }
    public int CurrentStatusId { get; set; }
    public DateTime? ExitTime { get; set; }
    public int VisitorStatusId { get; set; }

    #endregion

    #region Constructor
    public UpdateVisitorLogCommand(int visitorLogId, int currentStatusId, DateTime? exitTime, int visitorStatusId)
    {
        VisitorLogId = visitorLogId;
        CurrentStatusId = currentStatusId;
        ExitTime = exitTime;
        VisitorStatusId = visitorStatusId;
    }

    #endregion

}
