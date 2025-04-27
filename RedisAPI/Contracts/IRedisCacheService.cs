namespace RedisAPI.Contracts;

public interface IRedisCacheService
{
    Task SetCacheValueAsync(string key, object value);
    Task<T> GetCacheValueAsync<T>(string key);
}