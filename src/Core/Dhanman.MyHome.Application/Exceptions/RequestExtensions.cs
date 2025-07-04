using Dhanman.Shared.Contracts.Abstractions.Messaging;
using MediatR;

namespace Dhanman.MyHome.Application.Exceptions;

public static class RequestExtensions
{
    /// <summary>
    /// Checks if the request is a command.
    /// </summary>
    /// <typeparam name="TResponse">The response type.</typeparam>
    /// <param name="request">The request.</param>
    /// <returns>True if the request is a command, otherwise false.</returns>
    public static bool IsCommand<TResponse>(this IRequest<TResponse> request)
        => request is ICommand<TResponse>;

    /// <summary>
    /// Checks if the request is a query.
    /// </summary>
    /// <typeparam name="TResponse">The response type.</typeparam>
    /// <param name="request">The request.</param>
    /// <returns>True if the request is a query, otherwise false.</returns>
    public static bool IsQuery<TResponse>(this IRequest<TResponse> request)
        => request is IQuery<TResponse>;

    /// <summary>
    /// Checks if the request is a cacheable query.
    /// </summary>
    /// <typeparam name="TResponse">The response type.</typeparam>
    /// <param name="request">The request.</param>
    /// <returns>True if the request is a cacheable query, otherwise false.</returns>
    public static bool IsCacheableQuery<TResponse>(this IRequest<TResponse> request)
        => request is ICacheableQuery<TResponse>;
}