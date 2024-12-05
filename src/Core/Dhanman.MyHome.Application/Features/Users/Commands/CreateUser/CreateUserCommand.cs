using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.Users.Commands.CreateUser;

public sealed class CreateUserCommand : ICommand<Result<EntityCreatedResponse>>
{
    #region Properties
    public Guid Id { get; }
    public Guid CompanyId { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string Email { get; }
    public string PhoneNumber { get; }
    public string PasswordHash { get; }
    public bool IsOwner { get; }

    #endregion

    #region Constructors
    public CreateUserCommand(Guid id, Guid companyId, string firstName, string lastName, string email, string phoneNumber, bool isOwner)
    {
        Id = id;
        CompanyId = companyId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        IsOwner = isOwner;
    }
    #endregion
}
