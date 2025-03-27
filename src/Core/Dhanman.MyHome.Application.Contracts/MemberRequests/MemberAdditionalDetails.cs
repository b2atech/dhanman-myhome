namespace Dhanman.MyHome.Application.Contracts.MemberRequests;

public class MemberAdditionalDetails
{
    #region Properties    
    public string MemberType { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public Guid HattyId { get; set; }
    public string CompanyName { get; set; }
    public string Designation { get; set; }
    public DateTime DateOfBirth { get; set; }
    public char Gender { get; set; }
    public string MaritalStatus { get; set; }
    public string AboutYourSelf { get; set; }
    public string SpouseName { get; set; }
    public Guid SpouseHattyId { get; set; }

    #endregion

    #region Constructor    
     
    #endregion
}