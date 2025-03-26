using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.MemberRequests;

public class MemberRequest : EntityInt, IAuditableEntity, ISoftDeletableEntity
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
    public Guid CurrentAddressId { get; set; }
    public DateTime DateOfBirth { get; set; }
    public char Gender { get; set; }
    public string MaritalStatus { get; set; }
    public string AboutYourSelf { get; set; }
    public string SpouseName { get; set; }
    public Guid SpouseHattyId { get; set; }
    public int CommunityRequestStatusId { get; set; }
    #endregion

    #region Audit Properties
    public DateTime CreatedOnUtc { get; }

    public DateTime? ModifiedOnUtc { get; }

    public DateTime? DeletedOnUtc { get; }

    public bool IsDeleted { get; }

    public Guid CreatedBy { get; protected set; }

    public Guid? ModifiedBy { get; protected set; }
    #endregion

    #region Constructor 
    public MemberRequest(int id, string memberType, string userName, string password, string firstName, string lastName, Guid hattyId, string email, string mobileNumber, string companyName, string designation, Guid currentAddressId, DateTime dateOfBirth, char gender, string maritalStatus, string aboutYourSelf, string spouseName, Guid spouseHattyId, int communityRequestStatusId)
    {
        Id = id; 
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
        CurrentAddressId = currentAddressId;
        DateOfBirth = dateOfBirth;
        Gender = gender;
        MaritalStatus = maritalStatus;
        AboutYourSelf = aboutYourSelf;
        SpouseName = spouseName;
        SpouseHattyId = spouseHattyId;
        CommunityRequestStatusId = communityRequestStatusId;
    } 
    #endregion
}