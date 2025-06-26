using Dhanman.MyHome.Application.Abstractions;
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
                var command = JsonConvert.DeserializeObject<TCommand>(message);
                if (command == null)
                {
                    _logger.LogWarning($"{source} deserialized to null.");
                    return;
                }

                using var scope = _scopeFactory.CreateScope();
                var handler = scope.ServiceProvider.GetRequiredService<IMessageHandler<TCommand>>();
                await handler.HandleAsync(command);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error handling {source} message.");
            }
        }
    }

}
