//using Dhanman.MyHome.Application.Abstractions;
//using Dhanman.Shared.Contracts.Events;
//using Microsoft.Extensions.Logging;

//namespace Dhanman.MyHome.Application.Handlers;

//public class UserCreatedEventHandler : IMessageHandler<UserCreatedEvent>
//{
//    private readonly ILogger<UserCreatedEventHandler> _logger;

//    public UserCreatedEventHandler(ILogger<UserCreatedEventHandler> logger)
//    {
//        _logger = logger;
//    }

//    public Task HandleAsync(UserCreatedEvent userCreatedEvent)
//    {


//        if (userCreatedEvent == null)
//        {
//            _logger.LogWarning("Failed to deserialize UserCreatedEvent.");
//            return Task.CompletedTask;
//        }

//        _logger.LogInformation($"Processing UserCreatedEvent for UserId: {userCreatedEvent.UserId}");

//        // Your logic here, e.g., create a user in sales DB, etc.

//        return Task.CompletedTask;
//    }
//}