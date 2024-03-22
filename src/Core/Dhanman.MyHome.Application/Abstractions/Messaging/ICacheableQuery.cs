namespace Dhanman.MyHome.Application.Abstractions.Messaging;

public interface ICacheableQuery<out TResponse> : IQuery<TResponse>
{
    /// <summary>
    /// Gets the query cache key.
    /// </summary>
    /// <returns>The cache key.</returns>
    string GetCacheKey();

    /// <summary>
    /// Gets the query cache time.
    /// </summary>
    /// <returns>The cache time in minutes.</returns>
    int GetCacheTime() => 10;
}