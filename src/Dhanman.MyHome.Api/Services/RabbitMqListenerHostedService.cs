using B2aTech.CrossCuttingConcern.Messaging.RabbitMQ.Abstractions;
using B2aTech.CrossCuttingConcern.Messaging.RabbitMQ.Configurations;
using B2aTech.CrossCuttingConcern.Messaging.RabbitMQ.Extensions;
using Microsoft.Extensions.Options;

namespace Dhanman.MyHome.Api.Services;

public class RabbitMqListenerHostedService : BackgroundService, IRabbitMqListenerHostedService
{
    private readonly ILogger<RabbitMqListenerHostedService> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly RabbitMqOptions _options;

    public RabbitMqListenerHostedService(ILogger<RabbitMqListenerHostedService> logger, IServiceScopeFactory serviceScopeFactory,
        IOptions<RabbitMqOptions> options)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
        _options = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("RabbitMQ Listener Hosted Service is starting.");

        using var scope = _serviceScopeFactory.CreateScope();

        var commandConsumer = scope.ServiceProvider.GetRequiredService<ICommandConsumer>();
        var eventConsumer = scope.ServiceProvider.GetRequiredService<IEventConsumer>();

        await StartCommandConsumersAsync(commandConsumer, stoppingToken);
        await StartEventConsumerAsync(eventConsumer, stoppingToken);

        // Wait indefinitely until cancellation is requested
        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    // Start consuming event queue
    private async Task StartCommandConsumersAsync(ICommandConsumer commandConsumer, CancellationToken token)
    {
        var communityCommandQueue = _options.GetQueue("commands");
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
                _logger.LogInformation($"✅ Dispatching handler for: {routingKey}");
                await handler(message);
            }
            else
            {
                _logger.LogWarning($"⚠️ No handler for: {routingKey}");
            }
        },
         token);
    }


    private async Task StartEventConsumerAsync(IEventConsumer eventConsumer, CancellationToken cancellationToken)
    {
        var communityEventQueue = _options.GetQueue("events");

        using var scope = _serviceScopeFactory.CreateScope();
        var handlerInstance = scope.ServiceProvider.GetRequiredService<RabbitMqEventHandlers>();

        await eventConsumer.StartConsumingAsync(
            communityEventQueue,
            async message => await handlerInstance.HandleAsync(message),
            cancellationToken);
    }
    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("RabbitMQ Listener Hosted Service is stopping.");
        await base.StopAsync(cancellationToken);
    }
}