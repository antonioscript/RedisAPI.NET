// Controllers/RedisCacheController.cs
using Microsoft.AspNetCore.Mvc;
using RedisAPI.Contracts;

namespace RedisAPI.Controllers;

/*
    Controller que mostra como funciona o sistema
    de cache distribuído do Redis
 */

[ApiController]
[Route("api/[controller]")]
public class RedisCacheController : ControllerBase
{
    private readonly IRedisCacheService _redisCacheService;

    public RedisCacheController(IRedisCacheService redisCacheService)
    {
        _redisCacheService = redisCacheService;
    }

    [HttpPost("set/{key}")]
    public async Task<IActionResult> SetCacheValue(string key, [FromBody] object value)
    {
        await _redisCacheService.SetCacheValueAsync(key, value);
        return Ok($"Valor armazenado no cache com chave '{key}'.");
    }

    [HttpGet("get/{key}")]
    public async Task<IActionResult> GetCacheValue(string key)
    {
        var value = await _redisCacheService.GetCacheValueAsync<object>(key);

        if (value == null)
        {
            return NotFound($"Chave '{key}' não encontrada no cache.");
        }

        return Ok($"Valor do cache para '{key}': {value}");
    }
}
