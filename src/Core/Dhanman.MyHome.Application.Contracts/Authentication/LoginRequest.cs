namespace Dhanman.MyHome.Application.Contracts.Authentication;

public sealed class LoginRequest
{
    public LoginRequest()
    {
        Email = string.Empty;
        Password = string.Empty;
    }

    public string Email { get; set; }

    public string Password { get; set; }
}