﻿using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Authentication;

namespace Dhanman.MyHome.Application.Features.Authentication.Commands.Login;

public sealed class LoginCommand : ICommand<Result<TokenResponse>>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LoginCommand"/> class.
    /// </summary>
    /// <param name="email">The email.</param>
    /// <param name="password">The password.</param>
    public LoginCommand(string email, string password)
    {
        Email = email;
        Password = password;
    }

    /// <summary>
    /// Gets the email.
    /// </summary>
    public string Email { get; }

    /// <summary>
    /// Gets the password.
    /// </summary>
    public string Password { get; }
}
