using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.VisitorApprovals.Commands.UpdateVisitorApproval;

public class UpdateVisitorApproveCommand : ICommand<Result<EntityUpdatedResponse>>
{
    #region Properties
    public int VisitorApproveId { get; set; }
    public int VisitTypeId { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public TimeOnly? EntryTime { get; set; }
    public TimeOnly? ExitTime { get; set; }
    public string? VehicleNumber { get; set; }
    public string? CompanyName { get; set; }
    public Guid CreatedBy { get; set; }
    #endregion

    #region Constructor
    public UpdateVisitorApproveCommand(int visitorApproveId, int visitTypeId, DateOnly? startDate, DateOnly? endDate, TimeOnly? entryTime,
       TimeOnly? exitTime, string? vehicleNumber, string? companyName, Guid createdBy)
    {
        VisitorApproveId = visitorApproveId;
        VisitTypeId = visitTypeId;
        StartDate = startDate;
        EndDate = endDate;
        EntryTime = entryTime;
        ExitTime = exitTime;
        VehicleNumber = vehicleNumber;
        CompanyName = companyName;
        CreatedBy = createdBy;
    }
    #endregion
}
