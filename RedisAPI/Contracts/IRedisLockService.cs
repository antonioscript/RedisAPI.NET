namespace RedisAPI.Contracts;
public interface IRedisLockService
{
    Task<bool> ExecuteWithLockAsync(string lockKey, TimeSpan expiry, Func<Task> action);
}
