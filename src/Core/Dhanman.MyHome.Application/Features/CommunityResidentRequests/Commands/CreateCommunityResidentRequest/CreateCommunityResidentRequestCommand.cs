using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Contracts.ServiceProviders;

namespace Dhanman.MyHome.Application.Features.CommunityResidentRequests.Commands.CreateCommunityResidentRequest;

public class CreateCommunityResidentRequestCommand : ICommand<Result<EntityCreatedResponse>>
{
    #region Properties
    public string MemberType { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Guid HattyId { get; set; }
    public string Email { get; set; }
    public string MobileNumber { get; set; }
    public string CompanyName { get; set; }
    public string Designation { get; set; }
    public Address CurrentAddress { get; set; }
    public DateTime DateOfBirth { get; set; }
    public char Gender { get; set; }
    public string MaritalStatus { get; set; }
    public string AboutYourSelf { get; set; }
    public string SpouseName { get; set; }
    public Guid SpouseHattyId { get; set; }
    #endregion

    #region Constructors
    public CreateCommunityResidentRequestCommand(string memberType, string userName, string password, string firstName, string lastName, Guid hattyId, string email, string mobileNumber, string companyName, string designation, Address currentAddress, DateTime dateOfBirth, char gender, string maritalStatus, string aboutYourSelf, string spouseName, Guid spouseHattyId)
    {
        MemberType = memberType;
        UserName = userName;
        Password = password;
        FirstName = firstName;
        LastName = lastName;
        HattyId = hattyId;
        Email = email;
        MobileNumber = mobileNumber;
        CompanyName = companyName;
        Designation = designation;
        CurrentAddress = currentAddress;
        DateOfBirth = dateOfBirth;
        Gender = gender;
        MaritalStatus = maritalStatus;
        AboutYourSelf = aboutYourSelf;
        SpouseName = spouseName;
        SpouseHattyId = spouseHattyId;
    }
    #endregion
}