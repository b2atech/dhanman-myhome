using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.Authentication.Commands.Register;

public sealed class RegisterCommand : ICommand<Result<EntityCreatedResponse>>
{
    public RegisterCommand(string firstName, string lastName, string email, string password, string confirmPassword)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        ConfirmPassword = confirmPassword;
    }

    /// <summary>
    /// Gets the first name.
    /// </summary>
    public string FirstName { get; }

    /// <summary>
    /// Gets the last name.
    /// </summary>
    public string LastName { get; }

    /// <summary>
    /// Gets the email.
    /// </summary>
    public string Email { get; }

    /// <summary>
    /// Gets the password.
    /// </summary>
    public string Password { get; }

    /// <summary>
    /// Gets the confirmed password.
    /// </summary>
    public string ConfirmPassword { get; }
}
