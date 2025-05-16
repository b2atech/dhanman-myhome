namespace Dhanman.MyHome.Application.Contracts.Users;

public class UserIdResponse
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}