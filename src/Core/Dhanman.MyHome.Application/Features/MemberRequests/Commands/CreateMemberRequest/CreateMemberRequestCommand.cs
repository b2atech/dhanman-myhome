using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Contracts.MemberRequests;
using Dhanman.MyHome.Application.Contracts.ServiceProviders;

namespace Dhanman.MyHome.Application.Features.MemberRequests.Commands.CreateMemberRequest;

public class CreateMemberRequestCommand : ICommand<Result<EntityCreatedResponse>>
{
    #region Properties
    public Guid ApartmentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }   
    public string Email { get; set; }
    public string ContactNumber { get; set; }    
    public MemberAdditionalDetails MemberAdditionalDetails { get; set; }
    public Address CurrentAddress { get; set; }
    #endregion

    #region Constructors
   
    public CreateMemberRequestCommand(Guid apartmentId, string firstName, string lastName, string email, string contactNumber, MemberAdditionalDetails memberAdditionalDetails, Address currentAddress = null)//, DateTime dateOfBirth, char gender, string maritalStatus, string aboutYourSelf, string spouseName, Guid spouseHattyId)
    {
        ApartmentId = apartmentId;
        FirstName = firstName;
        LastName = lastName;      
        Email = email;
        ContactNumber = contactNumber;      
        MemberAdditionalDetails = memberAdditionalDetails;
        CurrentAddress = currentAddress ?? null;       
    }    
    #endregion
}