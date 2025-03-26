using Dhanman.MyHome.Application.Contracts.ServiceProviders;

namespace Dhanman.MyHome.Application.Contracts.MemberRequests;

public class CreateMemberRequestRequest
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
    public CreateMemberRequestRequest() => FirstName = string.Empty;
    #endregion
}