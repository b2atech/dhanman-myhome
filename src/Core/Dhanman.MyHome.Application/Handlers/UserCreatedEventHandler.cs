using Dhanman.MyHome.Application.Abstractions;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Users;
using Dhanman.Shared.Contracts.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace Dhanman.MyHome.Application.Handlers;

public class UserCreatedEventHandler : IMessageHandler<UserCreatedEvent>
{
    #region Properties
    private readonly IUserRepository _userRepository;
    private readonly IMediator _mediator;
    private readonly ILogger<UserCreatedEventHandler> _logger;
    #endregion

    public UserCreatedEventHandler(IUserRepository userRepository, IMediator mediator, ILogger<UserCreatedEventHandler> logger)
    {
        _logger = logger;
        _userRepository = userRepository;
        _mediator = mediator;
    }

    public Task HandleAsync(UserCreatedEvent userCreatedEvent)
    {
        if (userCreatedEvent == null)
        {
            _logger.LogWarning("Failed to deserialize UserCreatedEvent.");
            return Task.CompletedTask;
        }

        _logger.LogInformation($"Processing UserCreatedEvent for UserId: {userCreatedEvent.UserId}");
        var user = new User(userCreatedEvent.UserId, userCreatedEvent.CompanyId, new FirstName(userCreatedEvent.FirstName), new LastName(userCreatedEvent.LastName), new Email(userCreatedEvent.Email), new ContactNumber(userCreatedEvent.PhoneNumber), false);
        _userRepository.Insert(user);

        return Task.CompletedTask;
    }
}
