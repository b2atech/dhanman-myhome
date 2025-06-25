using Dhanman.MyHome.Application.Abstractions;
using Dhanman.MyHome.Application.Constants;
using Dhanman.Shared.Contracts.Events;
using Newtonsoft.Json;

namespace Dhanman.MyHome.Api.Services;

public class RabbitMqEventHandlers
{
    #region Properties
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<RabbitMqEventHandlers> _logger;

    #endregion

    #region Constructor
    public RabbitMqEventHandlers(IServiceScopeFactory serviceScopeFactory, ILogger<RabbitMqEventHandlers> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }
    #endregion

    #region Methods
    public async Task HandleCommonBasicCompanyCreatedEventAsync(string message)
    {
        await HandleMessageAsync<CreateCommonBasicCompanyEvent>(message, RoutingKeys.CommonBasicCompanyCreated);
    }
    private async Task HandleMessageAsync<TEvent>(string message, string source) where TEvent : class
    {
        _logger.LogInformation("[Event] {Source} received: {Message}", source, message);

        try
        {
            var @event = JsonConvert.DeserializeObject<TEvent>(message);
            if (@event == null)
            {
                _logger.LogWarning("{Source} deserialized to null.", source);
                return;
            }

            using var scope = _serviceScopeFactory.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService<IMessageHandler<TEvent>>();
            await handler.HandleAsync(@event);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error handling {Source} event.", source);
        }
    }
    public async Task HandleUserCreatedEventAsync(string message)
    {
        await HandleMessageAsync<UserCreatedEvent>(message, RoutingKeys.CommonUserCreated);
    }

    public async Task HandleUserCreatedEventAsync1(string message)
    {
        _logger.LogInformation("[Event] user.created received: {Message}", message);

        try
        {
            var userCreatedEvent = JsonConvert.DeserializeObject<UserCreatedEvent>(message);
            if (userCreatedEvent == null)
            {
                _logger.LogWarning("UserCreatedEvent deserialized to null.");
                return;
            }

            using var scope = _serviceScopeFactory.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService<IMessageHandler<UserCreatedEvent>>();
            await handler.HandleAsync(userCreatedEvent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error handling UserCreatedEvent.");
        }
    }

    #endregion
}
