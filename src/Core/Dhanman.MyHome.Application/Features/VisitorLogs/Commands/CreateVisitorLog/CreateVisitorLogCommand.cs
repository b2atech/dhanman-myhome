using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Contracts.VisitorLogs;

namespace Dhanman.MyHome.Application.Features.VisitorLogs.Commands.CreateVisitorLog;

public sealed class CreateVisitorLogCommand : ICommand<Result<EntityCreatedResponse>>
{
    #region Properties
    public int VisitorId { get; }
    public List<VisitorLog> VisitorLogDetails { get; } 
    #endregion

    #region Constructor

    public CreateVisitorLogCommand(int visitorId, List<VisitorLog> visitorLogDetails)
    {
        VisitorId = visitorId;
        VisitorLogDetails = visitorLogDetails;
    }     
    #endregion

}