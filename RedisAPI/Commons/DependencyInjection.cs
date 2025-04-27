using RedisAPI.Contracts;
using RedisAPI.Implements;

namespace RedisAPI.Commons;
public static class DependencyInjection
{
    public static void AddRedisServices(this IServiceCollection services)
    {
        services.AddScoped<IRedisService, RedisService>();
        services.AddScoped<IRedisCacheService, RedisCacheService>();
        services.AddScoped<IRedisLockService, RedisLockService>();
    }
}
