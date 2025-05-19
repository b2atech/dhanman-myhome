namespace Dhanman.MyHome.Application.Contracts.Residents;

public sealed class ResidentNameResponse
{
    #region Properties 
    public int Id { get; }    
    public string FirstName { get; }
    public string LastName { get; }
    public string ResidentName { get; }
    public Guid UserId { get; }

    #endregion

    #region Constructor
    public ResidentNameResponse(int id, string firstName, string lastName,Guid userId)
    {
        Id = id;        
        FirstName = firstName;
        LastName = lastName;
        ResidentName = $"{firstName} {lastName}";
        UserId = userId;
    }
    #endregion
}