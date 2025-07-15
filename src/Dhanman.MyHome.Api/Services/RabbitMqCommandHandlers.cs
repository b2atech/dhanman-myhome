using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Messaging;
using B2aTech.CrossCuttingConcern.Messaging.RabbitMQ.Models;
using Dhanman.Shared.Contracts.Commands;
using MediatR;
using Newtonsoft.Json;

namespace Dhanman.MyHome.Api.Services
{
    public class RabbitMqCommandHandlers
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<RabbitMqCommandHandlers> _logger;

        public RabbitMqCommandHandlers(IServiceScopeFactory scopeFactory, ILogger<RabbitMqCommandHandlers> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        private async Task HandleMessageAsync<TCommand>(string message, string source) where TCommand : class
        {
            _logger.LogInformation($"[Command] {source} received: {message}");

            try
            {
                var envelope = JsonConvert.DeserializeObject<CommandEnvelope<TCommand>>(message);
                if (envelope?.Payload == null)
                {
                    _logger.LogWarning($"{source} deserialized to null or missing payload.");
                    return;
                }
                MessageContext context = new()
                {
                    CorrelationId = envelope.CorrelationId,
                    UserId = envelope.UserId,
                    OrganizationId = envelope.OrganizationId
                };
                var command = envelope.Payload;

                // Create a new scope for resolving services (including IMediator) from DI container
                using (var scope = _scopeFactory.CreateScope())
                {
                    // Resolve IMediator inside the scope
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();


                    // Now, instead of manually resolving the handler, we use MediatR
                    await mediator.Send(command, new CancellationToken());

                }
                _logger.LogInformation($"✅ Successfully handled {source}.");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"❌ Error handling {source} message.");
            }
        }
    }

}
