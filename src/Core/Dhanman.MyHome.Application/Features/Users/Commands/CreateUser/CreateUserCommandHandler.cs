﻿using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.Users.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Users;
using MediatR;

namespace Dhanman.MyHome.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Result<EntityCreatedResponse>>
{
    #region Properties
    private readonly IUserRepository _userRepository;
    private readonly IMediator _mediator;
    #endregion

    #region Constructors
    public CreateUserCommandHandler(IUserRepository userRepository, IMediator mediator)
    {
        _userRepository = userRepository;
        _mediator = mediator;
    }
    #endregion

    #region Methods
    public async Task<Result<EntityCreatedResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(request.Id, request.CompanyId, new FirstName(request.FirstName), new LastName(request.LastName), new Email(request.Email), new ContactNumber(request.PhoneNumber), request.IsOwner);

        _userRepository.Insert(user);

        await _mediator.Publish(new UserCreatedEvent(user.Id), cancellationToken);

        return Result.Success(new EntityCreatedResponse(user.Id));
    }

    #endregion
}
