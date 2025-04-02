using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.VisitorLogs.Commands.CreateVisitorLog;

public sealed class CreateVisitorLogCommand : ICommand<Result<EntityCreatedResponse>>
{
    #region Properties    
    public int VisitorId { get; set; }
    public List<int> VisitingUnitIds { get; set; }
    public int? VisitorTypeId { get; set; }
    public string VisitingFrom { get; set; }
    public DateTime EntryTime { get; set; }
    public DateTime? ExitTime { get; set; }
    public int VisitorStatusId { get; set; }
    #endregion

    #region Constructor
    public CreateVisitorLogCommand(int visitorId, List<int> visitingUnitIds, int? visitorTypeId, string visitingFrom, DateTime entryTime, DateTime? exitTime, int visitorStatusId)
    {        
        VisitorId = visitorId;
        VisitingUnitIds = visitingUnitIds;
        VisitorTypeId = visitorTypeId;
        VisitingFrom = visitingFrom;
        EntryTime = entryTime;
        ExitTime = exitTime;
        VisitorStatusId = visitorStatusId;
    }

    #endregion

}