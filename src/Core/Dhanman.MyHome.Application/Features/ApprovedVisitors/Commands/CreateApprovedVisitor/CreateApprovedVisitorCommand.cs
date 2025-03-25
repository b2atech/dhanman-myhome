using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.ApprovedVisitors.Commands.CreateApprovedVisitor;

public class CreateApprovedVisitorCommand : ICommand<Result<EntityCreatedResponse>>
{
    #region Properties
    public int VisitorId { get; set; }
    public int VisitTypeId { get; set; }  
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public TimeOnly? EntryTime { get; set; }
    public TimeOnly? ExitTime { get; set; }

    public CreateApprovedVisitorCommand(int visitorId, int visitTypeId, DateOnly? startDate, DateOnly? endDate, TimeOnly? entryTime, TimeOnly? exitTime)
    {
        VisitorId = visitorId;
        VisitTypeId = visitTypeId;
        StartDate = startDate;
        EndDate = endDate;
        EntryTime = entryTime;
        ExitTime = exitTime;
    }

    public CreateApprovedVisitorCommand() { }
    #endregion
}