namespace Dhanman.MyHome.Api.Services
{
    public interface IRabbitMqListenerHostedService
    {
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
    }
}
