using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.Visitors.Commands.CreateVisitorAndLog;

public class CreateVisitorWithPendingApprovalCommand : ICommand<Result<EntityCreatedResponse>>
{
    #region Properties
    public int VisitorId { get; set; }
    public Guid ApartmentId { get; set; }
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public string ContactNumber { get; set; }
    public int VisitorTypeId { get; set; }
    public List<int> UnitIds { get; set; }
    public Guid CreatedBy { get; set; }

    #endregion

    #region Constructors
    public CreateVisitorWithPendingApprovalCommand(Guid apartmentId, string firstName, string? lastName, string contactNumber, int visitorTypeId,   Guid createdBy, List<int> unitIds)
    {
        ApartmentId = apartmentId;
        FirstName = firstName;
        LastName = lastName;
        ContactNumber = contactNumber;
        VisitorTypeId = visitorTypeId;
        CreatedBy = createdBy;
        UnitIds = unitIds;
    }

    #endregion
}
