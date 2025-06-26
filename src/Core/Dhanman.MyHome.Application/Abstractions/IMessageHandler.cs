namespace Dhanman.MyHome.Application.Abstractions
{
    public interface IMessageHandler<T>
    {
        Task HandleAsync(T message);
    }
}
