using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Result;
using B2aTech.CrossCuttingConcern.Messaging;
using B2aTech.CrossCuttingConcern.Messaging.RabbitMQ.Models;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Commands;
using Dhanman.Shared.Contracts.Common;
using Dhanman.Shared.Contracts.Events;
using MediatR;
using Newtonsoft.Json;
using System.Reflection;


namespace Dhanman.MyHome.Api.Services;

public class RabbitMqEventHandlers
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<RabbitMqEventHandlers> _logger;
    private readonly IMediator _mediator; // Inject Mediator


    public RabbitMqEventHandlers(IServiceScopeFactory scopeFactory, ILogger<RabbitMqEventHandlers> logger, IMediator mediator)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
        _mediator = mediator; // Initialize Mediator
    }

    public async Task HandleAsync(string message)
    {
        try
        {
            // Deserialize the EventEnvelope as a generic object first, since the payload can be of any type
            var envelope = JsonConvert.DeserializeObject<EventEnvelope>(message);

            if (envelope == null || string.IsNullOrWhiteSpace(envelope.EventType))
            {
                _logger.LogWarning("⚠️ Missing or invalid EventType in envelope.");
                return;  // Exit early if envelope is invalid
            }

            _logger.LogInformation($"📥 Received Event: {envelope.EventType} from {envelope.Source}");

            // Create the MessageContext from the envelope (user and correlation IDs)
            var context = new MessageContext
            {
                UserId = envelope.UserId,
                CorrelationId = envelope.CorrelationId
            };

            // Dynamically resolve the correct command type based on EventType
            var commandType = GetCommandTypeFromEventType(envelope.EventType);

            if (commandType != null)
            {
                // Deserialize the payload into the correct command type dynamically
                var payload = JsonConvert.DeserializeObject(envelope.Payload.ToString(), commandType);

                if (payload != null)
                {
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        // Resolve IMediator inside the scope
                        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                        // Now, instead of manually resolving the handler, we use MediatR
                        await mediator.Send(payload, new CancellationToken());
                    }
                    // Dispatch the command with MediatR

                    _logger.LogInformation($"Successfully handled {envelope.EventType}.");
                }
                else
                {
                    _logger.LogWarning($"⚠️ Failed to deserialize payload into command type: {commandType.Name}");
                }
            }
            else
            {
                _logger.LogWarning($"⚠️ No handler mapped for EventType: {envelope.EventType}");
            }
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "❌ Failed to deserialize EventEnvelope.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Error processing the event message.");
        }
    }


    // Helper method to get the command type based on the event type
    private Type GetCommandTypeFromEventType(string eventType)
    {
        // Use the assembly where your commands are defined
        var assembly = Assembly.GetAssembly(typeof(CreateBasicCompanyCommand));  // Use any public class as reference

        if (assembly == null)
        {
            _logger.LogError("Failed to load the assembly for commands.");
            return null;
        }

        // Dynamically construct the command name (e.g., CreateBasicCompanyCommand for event CreateBasicCompanyCreated)
        var commandName = eventType;

        // Try to find the corresponding command type based on the event type dynamically
        var commandType = assembly.GetTypes()
            .FirstOrDefault(t => t.IsClass && typeof(ICommand<Result<EntityCreatedResponse>>).IsAssignableFrom(t) && t.Name == commandName);

        return commandType;
    }
}


//public async Task HandleAsync(string message)
//{
//    EventEnvelope envelope;

//    try
//    {
//        envelope = JsonConvert.DeserializeObject<EventEnvelope>(message);
//    }
//    catch (Exception ex)
//    {
//        _logger.LogError(ex, "❌ Failed to deserialize EventEnvelope.");
//        return;
//    }

//    if (envelope == null || string.IsNullOrWhiteSpace(envelope.EventType))
//    {
//        _logger.LogWarning("⚠️ Missing or invalid EventType in envelope.");
//        return;
//    }

//    _logger.LogInformation("📥 Received Event: {EventType} from {Source}", envelope.EventType, envelope.Source);

//    var context = new MessageContext
//    {
//        UserId = envelope.UserId,
//        CorrelationId = envelope.CorrelationId
//    };

//    switch (envelope.EventType)
//    {
//        case EventTypes.CommonBasicCompanyCreated:
//            await DispatchTypedEvent<BasicCompanyCreatedEvent>(envelope.Payload, context);
//            break;

//        case EventTypes.CommonUserCreated:
//            await DispatchTypedEvent<UserCreatedEvent>(envelope.Payload, context);
//            break;

//        case EventTypes.CommonOrganizationCreated:
//            await DispatchTypedEvent<BasicOrganizationCreatedEvent>(envelope.Payload, context);
//            break;

//        // ➕ Add future event cases here...

//        default:
//            _logger.LogWarning("⚠️ No handler mapped for EventType: {EventType}", envelope.EventType);
//            break;
//    }
//}