using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.VisitorApprovals.Commands.CreateVisitorApproval;

public class CreateVisitorApprovalCommand : ICommand<Result<EntityCreatedResponse>>
{
    #region Properties
    public int VisitorApproveId { get; set; }
    public Guid ApartmentId { get; set; }
    public string FirstName { get; set; }
    public string ContactNumber { get; set; }
    public int VisitorTypeId { get; set; }
    public int VisitTypeId { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public TimeOnly? EntryTime { get; set; }
    public TimeOnly? ExitTime { get; set; }
    public Guid CreatedBy { get; set; }
    #endregion

    #region Constructors
    public CreateVisitorApprovalCommand(Guid apartmentId, string firstName, string contactNumber, int visitorTypeId, int visitTypeId, DateOnly? startDate, DateOnly? endDate, TimeOnly? entryTime, TimeOnly? exitTime, Guid createdBy)
    {
        ApartmentId = apartmentId;
        FirstName = firstName;
        ContactNumber = contactNumber;
        VisitorTypeId = visitorTypeId;
        VisitTypeId = visitTypeId;
        StartDate = startDate;
        EndDate = endDate;
        EntryTime = entryTime;
        ExitTime = exitTime;
        CreatedBy = createdBy;
    }

    public CreateVisitorApprovalCommand() { }
    #endregion
}
