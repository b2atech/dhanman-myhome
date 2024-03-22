using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Cryptography;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Users;

namespace Dhanman.MyHome.Application.Features.Authentication.Commands.Register;

internal class RegisterCommandHandler : ICommandHandler<RegisterCommand, Result<EntityCreatedResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<EntityCreatedResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = new User(Guid.NewGuid(),
                            FirstName.Create(request.FirstName).Value(),
                            LastName.Create(request.LastName).Value(),
                            Email.Create(request.Email).Value()
                            , _passwordHasher.HashPassword(Password.Create(request.Password).Value()));

        _userRepository.Insert(user);

        //await _mediator.Publish(new CustomerCreatedEvent(customer.Id), cancellationToken);

        return Result.Success(new EntityCreatedResponse(user.Id));
    }

}
