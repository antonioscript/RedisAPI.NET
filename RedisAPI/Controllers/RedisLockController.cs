using Microsoft.AspNetCore.Mvc;
using RedisAPI.Constants;
using RedisAPI.Contracts;

namespace RedisAPI.Controllers;

/*
    Controller que mostra como funciona o Red Lock
*/

[ApiController]
[Route("api/[controller]")]
public class RedisLockController : ControllerBase
{
    private readonly IRedisLockService _redisLockService;
    private const string LockKey = LockKeys.CriticalTask;

    public RedisLockController(IRedisLockService redisLockService)
    {
        _redisLockService = redisLockService;
    }

    [HttpPost("process")]
    public async Task<IActionResult> ProcessCriticalTask()
    {
        var success = await _redisLockService.ExecuteWithLockAsync(
            lockKey: LockKey,
            expiry: TimeSpan.FromSeconds(90),
            action: async () =>
            {
                await Task.Delay(1000); // Simula um processamento de 5 segundos
                Console.WriteLine("Tarefa crítica executada!");
            });

        if (!success)
        {
            return Conflict("Outro processo já está executando a tarefa.");
        }

        return Ok("Tarefa crítica realizada com sucesso.");
    }
}
