using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.Visitors.Commands.CreateVisitorAndLog;

public class CreateVisitorPendingCommand : ICommand<Result<EntityCreatedResponse>>
{
    #region Properties
    public int VisitorId { get; set; }
    public Guid ApartmentId { get; set; }
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? VisitingFrom { get; set; }
    public string ContactNumber { get; set; }
    public int VisitorTypeId { get; set; }
    public int CurrentStatusId { get; set; }
    public string? VehicleNumber { get; set; }
    public int? IdentityTypeId { get; set; }
    public string? IdentityNumber { get; set; }
    public Guid CreatedBy { get; set; }

    #endregion

    #region Constructors
    public CreateVisitorPendingCommand(Guid apartmentId, string firstName, string? lastName, string? email, string? visitingFrom, string contactNumber, int visitorTypeId, int currentStatusId, string? vehicleNumber, int? identityTypeId, string? identityNumber, Guid createdBy)
    {
        ApartmentId = apartmentId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        VisitingFrom = visitingFrom;
        ContactNumber = contactNumber;
        VisitorTypeId = visitorTypeId;
        CurrentStatusId = currentStatusId;
        VehicleNumber = vehicleNumber;
        IdentityTypeId = identityTypeId;
        IdentityNumber = identityNumber;
        CreatedBy = createdBy;
    }

    #endregion
}
