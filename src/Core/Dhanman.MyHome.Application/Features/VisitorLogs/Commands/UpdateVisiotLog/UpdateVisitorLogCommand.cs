using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.VisitorLogs.Commands.UpdateVisiotLog;

public class UpdateVisitorLogCommand : ICommand<Result<EntityUpdatedResponse>>
{
    #region Properties
    public List<int> VisitorLogIds { get; set; }
    #endregion

    #region Constructor
    public UpdateVisitorLogCommand(List<int> visitorLogIds)
    {
        VisitorLogIds = visitorLogIds;
    }
    #endregion

}
