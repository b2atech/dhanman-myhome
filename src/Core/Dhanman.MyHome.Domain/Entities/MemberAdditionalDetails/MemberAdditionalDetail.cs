using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.MemberAdditionalDetails;

public class MemberAdditionalDetail : Entity, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties
    public Guid Id { get; set; }
    public string MemberType { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string? CompanyName { get; set; }
    public string? Designation { get; set; }
    public Guid HattyId { get; set; }
    public DateTime DateOfBirth { get; }
    public Char Gender { get; set; }
    public string MaritalStatus { get; set; }
    public string? AboutYourself { get; set; }
    public string? SpouseName { get; set; }
    public Guid? SpouseHattyId { get; set; }
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; set; }
    public DateTime? DeletedOnUtc { get; }
    public bool IsDeleted { get; }
    public Guid CreatedBy { get; protected set; }
    public Guid? ModifiedBy { get; protected set; }

    #endregion

    #region Constructors
    public MemberAdditionalDetail(Guid id, string memberType, string userName, string password, string? companyName, string? designation, Guid hattyId, DateTime dateOfBirth, char gender, string maritalStatus, string? aboutYourself, string? spouseName, Guid? spouseHattyId)
    {
        Id = id;
        MemberType = memberType;
        UserName = userName;
        Password = password;
        CompanyName = companyName;
        Designation = designation;
        HattyId = hattyId;
        DateOfBirth = dateOfBirth;
        Gender = gender;
        MaritalStatus = maritalStatus;
        AboutYourself = aboutYourself;
        SpouseName = spouseName;
        SpouseHattyId = spouseHattyId;        
    }
    #endregion

}
//public class CommunityUserDetail : Entity, IAuditableEntity, ISoftDeletableEntity
//{
//    #region Properties
//    public Guid Id { get; set; }
//    public int ResidentId { get; set; }
//    public string? MemberType { get; set; }
//    public string? Designation { get; set; }
//    public Guid HattyId { get; set; }
//    public Guid CurrentAddressId { get; set; }
//    public DateTime DateOfBirth { get; }
//    public Char Gender { get; set; }
//    public string? MaritalStatus { get; set; }
//    public string? AboutYourself { get; set; }
//    public string? SpouseName { get; set; }
//    public Guid? SpouseHattyId { get; set; }

//    public DateTime CreatedOnUtc { get; }
//    public DateTime? ModifiedOnUtc { get; set; }
//    public DateTime? DeletedOnUtc { get; }
//    public bool IsDeleted { get; }
//    public Guid CreatedBy { get; protected set; }
//    public Guid? ModifiedBy { get; protected set; }


//    #endregion

//    #region Constructors
//    public CommunityUserDetail(Guid id, int residentId, string? memberType, string? designation, Guid hattyId, Guid currentAddressId, DateTime dateOfBirth, char gender, string? maritalStatus, string? aboutYourself, string? spouseName, Guid? spouseHattyId)
//    {
//        Id = id;
//        ResidentId = residentId;
//        MemberType = memberType;
//        Designation = designation;
//        HattyId = hattyId;
//        CurrentAddressId = currentAddressId;
//        DateOfBirth = dateOfBirth;
//        Gender = gender;
//        MaritalStatus = maritalStatus;
//        AboutYourself = aboutYourself;
//        SpouseName = spouseName;
//        SpouseHattyId = spouseHattyId;
//    }
//    #endregion

//}