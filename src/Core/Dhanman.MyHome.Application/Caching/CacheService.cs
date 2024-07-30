using System.Configuration;
using System.Runtime.Caching;

namespace Dhanman.MyHome.Application.Caching;

public class CacheService
{
    private readonly MemoryCache _cache;
    private readonly CacheItemPolicy _defaultPolicy;

    public CacheService()
    {
        _cache = MemoryCache.Default;
        _defaultPolicy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(Convert.ToInt16(ConfigurationManager.AppSettings["CacheExpiryTime"]))
        };
    }

    public T Get<T>(string key, Func<T> retrieveData)
    {
        if (_cache.Contains(key))
        {
            return (T)_cache.Get(key);
        }

        var data = retrieveData();
        _cache.Add(key, data, _defaultPolicy);
        return data;
    }

    public async Task<T> GetAsync<T>(string key, Func<Task<T>> retrieveData)
    {
        if (_cache.Contains(key))
        {
            return (T)_cache.Get(key);
        }

        var data = await retrieveData();
        _cache.Add(key, data, _defaultPolicy);
        return data;
    }

    public void Set<T>(string key, T value, CacheItemPolicy policy = null)
    {
        _cache.Set(key, value, policy ?? _defaultPolicy);
    }

    public void Remove(string key)
    {
        if (_cache.Contains(key))
        {
            _cache.Remove(key);
        }
    }
}