namespace Dhanman.MyHome.Application.Contracts.Users;

public sealed class CreateUserRequest
{
    #region Properties  
    public Guid UserId { get; set; }
    public Guid CompanyId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsOwner { get; set; }
    #endregion

    #region Constructors
    public CreateUserRequest() => LastName = string.Empty;
    #endregion
}
