using B2aTech.CrossCuttingConcern.Messaging.RabbitMQ.Abstractions;
using Dhanman.MyHome.Application.Constants;

namespace Dhanman.MyHome.Api.Services;

public class RabbitMqListenerHostedService : BackgroundService, IRabbitMqListenerHostedService
{
    #region Properties

    private readonly ILogger<RabbitMqListenerHostedService> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    #endregion

    #region Constructors

    public RabbitMqListenerHostedService(ILogger<RabbitMqListenerHostedService> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }
    #endregion

    #region Methods
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("RabbitMQ Listener Hosted Service is starting.");

        using var scope = _serviceScopeFactory.CreateScope();

        var commandConsumer = scope.ServiceProvider.GetRequiredService<ICommandConsumer>();
        var eventConsumer = scope.ServiceProvider.GetRequiredService<IEventConsumer>();

        await StartCommandConsumersAsync(commandConsumer, stoppingToken);
        await StartEventConsumerAsync(eventConsumer, stoppingToken);

    }
    private async Task StartCommandConsumersAsync(ICommandConsumer commandConsumer, CancellationToken token)
    {
        var communityCommandQueue = "community.commands";
        using var scope = _serviceScopeFactory.CreateScope();
        var handlerInstance = scope.ServiceProvider.GetRequiredService<RabbitMqCommandHandlers>();

        var handlerMap = new Dictionary<string, Func<string, Task>>
        {

            // Add more
        };
        await commandConsumer.StartConsumingAsync(communityCommandQueue, handlerMap.Keys, async (routingKey, message) =>
        {
            if (handlerMap.TryGetValue(routingKey, out var handler))
            {
                _logger.LogInformation("✅ Dispatching handler for: {RoutingKey}", routingKey);
                await handler(message);
            }
            else
            {
                _logger.LogWarning("⚠️ No handler for: {RoutingKey}", routingKey);
            }
        },
         token);
    }

    private async Task StartEventConsumerAsync(IEventConsumer eventConsumer, CancellationToken cancellationToken)
    {
        var communityEventQueue = "community.events";

        using var scope = _serviceScopeFactory.CreateScope();
        var handlerInstance = scope.ServiceProvider.GetRequiredService<RabbitMqEventHandlers>();

        var handlerMap = new Dictionary<string, Func<string, Task>>
        {
            [RoutingKeys.CommonBasicCompanyCreated] = handlerInstance.HandleCommonBasicCompanyCreatedEventAsync,
            [RoutingKeys.CommonUserCreated] = handlerInstance.HandleUserCreatedEventAsync
            // Add more event handler mappings
        };

        await eventConsumer.StartConsumingAsync(communityEventQueue, handlerMap.Keys, async (routingKey, message) =>
        {
            if (handlerMap.TryGetValue(routingKey, out var handler))
            {
                _logger.LogInformation("✅ Dispatching handler for: {RoutingKey}", routingKey);
                await handler(message);
            }
            else
            {
                _logger.LogWarning("⚠️ No event handler mapped for routing key: {RoutingKey}", routingKey);
            }
        }, cancellationToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("RabbitMQ Listener Hosted Service is stopping.");
        await base.StopAsync(cancellationToken);
    }
    #endregion

}
