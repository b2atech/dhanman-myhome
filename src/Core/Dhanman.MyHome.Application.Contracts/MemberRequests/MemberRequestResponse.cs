namespace Dhanman.MyHome.Application.Contracts.MemberRequests;

public sealed class MemberRequestResponse
{    
    #region Properties 
    public int Id { get; }
    public Guid ApartmentId { get; }
    public string ApartmentName { get; }    
    public string FirstName { get; }
    public string LastName { get; }    
    public string Email { get; }
    public string ContactNumber { get; }
    public Guid MemberAdditionalDetailsId { get; set; }
    public string MemberType { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public Guid HattyId { get; set; }
    public string HattyName { get; set; }
    public string CompanyName { get; set; }
    public string Designation { get; set; }
    public DateTime DateOfBirth { get; set; }
    public char Gender { get; set; }
    public string MaritalStatus { get; set; }
    public string AboutYourself { get; set; }
    public string SpouseName { get; set; }
    public Guid? SpouseHattyId { get; set; }
    public string SpouseHattyName { get; set; }
    public int RequestStatusId { get; }
    public string RequestStatus { get; }
    
    #endregion
    
    #region Constructor
    public MemberRequestResponse(int id, Guid apartmentId, string apartmentName, string firstName, string lastName, string email, string contactNumber, Guid memberAdditionalDetailsId, string? memberType, string? userName, string? password, Guid hattyId, string hattyName, string? companyName, string? designation, DateTime dateOfBirth, char gender, string? maritalStatus, string? aboutYourself, string? spouseName, Guid? spouseHattyId, string spouseHattyName, int requestStatusId, string requestStatus)
    {
        Id = id;
        ApartmentId = apartmentId;
        ApartmentName = apartmentName;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        ContactNumber = contactNumber;
        MemberAdditionalDetailsId = memberAdditionalDetailsId;
        MemberType = memberType;
        UserName = userName;
        Password = password;
        HattyId = hattyId;
        HattyName = hattyName;
        CompanyName = companyName;
        Designation = designation;
        DateOfBirth = dateOfBirth;
        Gender = gender;
        MaritalStatus = maritalStatus;
        AboutYourself = aboutYourself;
        SpouseName = spouseName;
        SpouseHattyId = spouseHattyId;
        SpouseHattyName = spouseHattyName;
        RequestStatusId = requestStatusId;
        RequestStatus = requestStatus;
    }    
    #endregion
}