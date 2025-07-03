using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.Visitors.Commands.UpdateVisitor;
 
public sealed class UpdateVisitorCommand : ICommand<Result<EntityUpdatedResponse>>
{
    #region Properties
    public int VisitorId { get; set; }
    public Guid ApartmentId { get; set; }
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; }
    public string VisitingFrom { get; set; }
    public string ContactNumber { get; set; }
    public int VisitorTypeId { get; set; }
    public string? VehicleNumber { get; set; }
    public int IdentityTypeId { get; set; }
    public string IdentityNumber { get; set; }
    #endregion

    #region Constructors
    public UpdateVisitorCommand(int id, Guid apartmentId, string firstName, string? lastName, string email, string visitingFrom, string contactNumber, int visitorTypeId, string? vehicleNumber, int identityTypeId, string identityNumber)
    {
        VisitorId = id;
        ApartmentId = apartmentId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        VisitingFrom = visitingFrom;
        ContactNumber = contactNumber;
        VisitorTypeId = visitorTypeId;
        VehicleNumber = vehicleNumber;
        IdentityTypeId = identityTypeId;
        IdentityNumber = identityNumber;
    }
    #endregion
}