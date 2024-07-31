namespace Dhanman.MyHome.Application.Contracts.Users;

public sealed class UserResponse
{
    #region Properties
    public Guid Id { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string ContactNumber { get; }
    public bool IsOwner { get; }
    public string Email { get; }
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; }
    public Guid CreatedBy { get; }
    public Guid? ModifiedBy { get; }

    #endregion

    #region Constructor
    public UserResponse(Guid id, string firstName, string lastName, string contactNumber, bool isOwner, string email, DateTime createdOnUtc, DateTime? modifiedOnUtc, Guid createdBy, Guid? modifiedBy)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        ContactNumber = contactNumber;
        IsOwner = isOwner;
        Email = email;
        CreatedOnUtc = createdOnUtc;
        ModifiedOnUtc = modifiedOnUtc;
        CreatedBy = createdBy;
        ModifiedBy = modifiedBy;
    }
    #endregion
}
