using Dhanman.MyHome.Application.Contracts.ServiceProviders;

namespace Dhanman.MyHome.Application.Contracts.MemberRequests;

public class CreateMemberRequestRequest
{
    #region Properties     
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string MobileNumber { get; set; }
    public MemberAdditionalDetails MemberAdditionalDetails { get; set; }
    public Address CurrentAddress { get; set; }
    #endregion

    #region Constructors
    public CreateMemberRequestRequest() => FirstName = string.Empty;
    #endregion
}