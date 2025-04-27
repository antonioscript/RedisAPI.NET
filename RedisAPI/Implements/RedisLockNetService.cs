using RedLockNet.SERedis;
using RedisAPI.Contracts;

namespace RedisAPI.Implements;

public class RedisLockNetService : IRedisLockService
{
    private readonly RedLockFactory _redLockFactory;

    public RedisLockNetService(RedLockFactory redLockFactory)
    {
        _redLockFactory = redLockFactory;
    }

    public async Task<bool> ExecuteWithLockAsync(string lockKey, TimeSpan expiry, Func<Task> action)
    {
        var redisLockKey = $"lock:{lockKey}";
        var wait = TimeSpan.FromSeconds(5);   // Quanto tempo esperar para tentar adquirir o lock
        var retry = TimeSpan.FromMilliseconds(500); // Tempo entre tentativas

        using (var redLock = await _redLockFactory.CreateLockAsync(redisLockKey, expiry, wait, retry))
        {
            if (!redLock.IsAcquired)
            {
                return false;
            }

            try
            {
                await action.Invoke();
            }
            finally
            {
                // RedLock libera automaticamente quando sai do using
            }
        }

        return true;
    }
}
