using MediatR;

namespace Dhanman.MyHome.Application.Abstractions.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
