using RedisAPI.Contracts;
using StackExchange.Redis;

namespace RedisAPI.Implements;
public class RedisService : IRedisService
{
    private readonly IConnectionMultiplexer _redis;

    public RedisService(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    public async Task SetValueAsync(string key, string value)
    {
        var db = _redis.GetDatabase();
        await db.StringSetAsync(key, value);
    }

    public async Task<string> GetValueAsync(string key)
    {
        var db = _redis.GetDatabase();
        return await db.StringGetAsync(key);
    }
}