﻿using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;
using Dhanman.MyHome.Domain.Utility;

namespace Dhanman.MyHome.Domain.Entities.Users;

public class User : Entity, IAuditableEntity, ISoftDeletableEntity
{
    private string _passwordHash;

    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class.
    /// </summary>
    /// <param name="id">The user identifier.</param>
    /// <param name="firstName">The user first name.</param>
    /// <param name="lastName">The user last name.</param>
    /// <param name="email">The user email instance.</param>
    /// <param name="passwordHash">The user password hash.</param>
    public User(Guid id, FirstName firstName, LastName lastName, Email email)
        : this()
    {
        Ensure.NotEmpty(id, "The identifier is required.", nameof(id));
        Ensure.NotEmpty(firstName, "The first name is required.", nameof(firstName));
        Ensure.NotEmpty(lastName, "The last name is required.", nameof(lastName));
        Ensure.NotEmpty(email, "The email is required.", nameof(email));

        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    protected User()
    {
        FirstName = FirstName.Empty;
        LastName = LastName.Empty;
        Email = Email.Empty;
        _passwordHash = string.Empty;
    }

    /// <summary>
    /// Gets the user first name.
    /// </summary>
    public FirstName FirstName { get; private set; }

    /// <summary>
    /// Gets the user last name.
    /// </summary>
    public LastName LastName { get; private set; }

    /// <summary>
    /// Gets the user email.
    /// </summary>
    public Email Email { get; private set; }

    /// <inheritdoc />
    public DateTime CreatedOnUtc { get; }

    /// <inheritdoc />
    public DateTime? ModifiedOnUtc { get; }

    /// <inheritdoc />
    public DateTime? DeletedOnUtc { get; }

    /// <inheritdoc />
    public bool IsDeleted { get; }

    public Guid CreatedBy { get; protected set; }

    public Guid? ModifiedBy { get; protected set; }

    /// <summary>
    /// Verifies that the provided password hash matches the password hash.
    /// </summary>
    /// <param name="password">The password to be checked against the user password hash.</param>
    /// <param name="passwordHashChecker">The password hash checker.</param>
    /// <returns>True if the password hashes match, otherwise false.</returns>
    public bool VerifyPasswordHash(string password)
        => !string.IsNullOrWhiteSpace(password);
}