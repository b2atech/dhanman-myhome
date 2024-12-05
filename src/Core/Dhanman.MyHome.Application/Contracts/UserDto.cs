namespace Dhanman.MyHome.Application.Contracts;

public class UserDto
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public UserDto(Guid id, Guid companyId, string firstName, string lastName, string email, string phoneNumber)
    {
        Id = id;
        CompanyId = companyId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
    }
}
