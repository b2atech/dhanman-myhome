using Dhanman.MyHome.Application.Abstractions;
using Newtonsoft.Json;

namespace Dhanman.MyHome.Api.Services;

public class RabbitMqCommandHandlers
{
	#region Properties
	private readonly IServiceScopeFactory _serviceScopeFactory;
	private readonly ILogger<RabbitMqCommandHandlers> _logger;
    #endregion

    #region Constructor
    public RabbitMqCommandHandlers(IServiceScopeFactory serviceScopeFactory, ILogger<RabbitMqCommandHandlers> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }
    #endregion

    #region Methods
    //TO BE used in RabbitMQ consumer to handle commands
    private async Task HandleMessageAsync<TCommand>(string message, string source) where TCommand : class
    {
        _logger.LogInformation("[Command] {Source} received: {Message}", source, message);

        try
        {
            var command = JsonConvert.DeserializeObject<TCommand>(message);
            if (command == null)
            {
                _logger.LogWarning("{Source} deserialized to null.", source);
                return;
            }

            using var scope = _serviceScopeFactory.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService<IMessageHandler<TCommand>>();
            await handler.HandleAsync(command);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error handling {Source} message.", source);
        }
    }
    #endregion
}
