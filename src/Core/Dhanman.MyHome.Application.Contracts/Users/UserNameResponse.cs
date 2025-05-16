namespace Dhanman.MyHome.Application.Contracts.Users;

public class UserNameResponse
{    public Guid UserId { get; set; }    
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public UserNameResponse(Guid userId, string firstName, string lastName)
    {
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
    }
}