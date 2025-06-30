using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Messaging;
using B2aTech.CrossCuttingConcern.Messaging.RabbitMQ.Models;
using Dhanman.Shared.Contracts.Events;
using Newtonsoft.Json;


namespace Dhanman.MyHome.Api.Services;

public class RabbitMqEventHandlers
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<RabbitMqEventHandlers> _logger;

    public RabbitMqEventHandlers(IServiceScopeFactory scopeFactory, ILogger<RabbitMqEventHandlers> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    public async Task HandleAsync(string message)
    {
        EventEnvelope envelope;

        try
        {
            envelope = JsonConvert.DeserializeObject<EventEnvelope>(message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Failed to deserialize EventEnvelope.");
            return;
        }

        if (envelope == null || string.IsNullOrWhiteSpace(envelope.EventType))
        {
            _logger.LogWarning("⚠️ Missing or invalid EventType in envelope.");
            return;
        }

        _logger.LogInformation("📥 Received Event: {EventType} from {Source}", envelope.EventType, envelope.Source);

        var context = new MessageContext
        {
            UserId = envelope.UserId,
            CorrelationId = envelope.CorrelationId
        };

        switch (envelope.EventType)
        {
            case EventTypes.CommonBasicCompanyCreated:
                await DispatchTypedEvent<BasicCompanyCreatedEvent>(envelope.Payload, context);
                break;

            case EventTypes.CommonUserCreated:
                await DispatchTypedEvent<UserCreatedEvent>(envelope.Payload, context);
                break;

            case EventTypes.CommonOrganizationCreated:
                await DispatchTypedEvent<BasicOrganizationCreatedEvent>(envelope.Payload, context);
                break;

            // ➕ Add future event cases here...

            default:
                _logger.LogWarning("⚠️ No handler mapped for EventType: {EventType}", envelope.EventType);
                break;
        }
    }

    private async Task DispatchTypedEvent<TEvent>(object payload, MessageContext context) where TEvent : class
    {
        try
        {
            var json = JsonConvert.SerializeObject(payload);
            var @event = JsonConvert.DeserializeObject<TEvent>(json);

            if (@event == null)
            {
                _logger.LogWarning("⚠️ Failed to deserialize payload into {EventType}", typeof(TEvent).Name);
                return;
            }

            using var scope = _scopeFactory.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService<IEventMessageHandler<TEvent>>();
            await handler.HandleAsync(@event, context);

            _logger.LogInformation("✅ Successfully handled event {EventType}", typeof(TEvent).Name);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Error dispatching typed event {EventType}", typeof(TEvent).Name);
        }
    }
}
