using Microsoft.Extensions.Caching.Distributed;
using RedisAPI.Contracts;
using System.Text.Json;

namespace RedisAPI.Implements;
public class RedisCacheService : IRedisCacheService
{
    private readonly IDistributedCache _cache;

    public RedisCacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task SetCacheValueAsync(string key, object value)
    {
        var options = new DistributedCacheEntryOptions()
            .SetAbsoluteExpiration(System.DateTime.Now.AddMinutes(5));

        var serializedValue = JsonSerializer.Serialize(value);
        await _cache.SetStringAsync(key, serializedValue, options);
    }

    public async Task<T> GetCacheValueAsync<T>(string key)
    {
        var serializedValue = await _cache.GetStringAsync(key);

        if (string.IsNullOrEmpty(serializedValue))
        {
            return default;
        }

        return JsonSerializer.Deserialize<T>(serializedValue);
    }
}
