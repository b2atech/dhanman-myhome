using Dhanman.MyHome.Application.Exceptions;
using Dhanman.MyHome.Domain.Abstractions;
using MediatR;
namespace Dhanman.MyHome.Application.Behaviors;

internal sealed class UnitOfWorkBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="UnitOfWorkBehaviour{TRequest,TResponse}"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work.</param>
    public UnitOfWorkBehaviour(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    ///// <inheritdoc />
    //public async Task<TResponse> Handle(
    //    TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    //{
    //    TResponse response = await next();

    //    if (request.IsQuery())
    //    {
    //        return response;
    //    }

    //    await _unitOfWork.SaveChangesAsync(cancellationToken);

    //    return response;
    //}

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var response = await next();
        if (request.IsQuery())
        {
            return response;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return response;
    }
}