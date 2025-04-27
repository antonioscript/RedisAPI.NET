using Microsoft.AspNetCore.Mvc;
using RedisAPI.Constants;
using RedisAPI.Contracts;
using RedisAPI.Implements;

namespace RedisAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RedisLockController : ControllerBase
{
    private readonly IRedisLockService _redisLockService;
    private readonly RedisLockNetService _redisLockNetService;

    private const string LockKey = LockKeys.CriticalTask;

    public RedisLockController(IRedisLockService redisLockService, RedisLockNetService redisLockNetService)
    {
        _redisLockService = redisLockService;
        _redisLockNetService = redisLockNetService;
    }

    [HttpPost("native-redis-process")]
    public async Task<IActionResult> ProcessCriticalTask()
    {
        var success = await _redisLockService.ExecuteWithLockAsync(
            lockKey: LockKey,
            expiry: TimeSpan.FromSeconds(90),
            action: async () =>
            {
                await Task.Delay(5000); // Simula processamento de 5 segundos
                Console.WriteLine("Tarefa crítica executada! [Nativo]");
            });

        if (!success)
        {
            return Conflict("Outro processo já está executando a tarefa.");
        }

        return Ok("Tarefa crítica realizada com sucesso.");
    }

    [HttpPost("net-process")]
    public async Task<IActionResult> ProcessCriticalTaskNet()
    {
        var success = await _redisLockNetService.ExecuteWithLockAsync(
            lockKey: LockKey,
            expiry: TimeSpan.FromSeconds(90),
            action: async () =>
            {
                await Task.Delay(5000); // Simula processamento de 5 segundos
                Console.WriteLine("Tarefa crítica executada! [RedLock.Net]");
            });

        if (!success)
        {
            return Conflict("Outro processo já está executando a tarefa usando RedLock.Net.");
        }

        return Ok("Tarefa crítica realizada com sucesso usando RedLock.Net.");
    }
}
