using Microsoft.AspNetCore.Mvc;
using RedisAPI.Contracts;

namespace RedisAPI.Controllers;

/*
    Controller simples que mostra apenas como
    inserir e pegar valores no banco de dados Redis
 */

[ApiController]
[Route("api/[controller]")]
public class RedisController : ControllerBase
{
    private readonly IRedisService _redisService;

    public RedisController(IRedisService redisService)
    {
        _redisService = redisService;
    }

    [HttpPost("set/{key}/{value}")]
    public async Task<IActionResult> SetValue(string key, string value)
    {
        await _redisService.SetValueAsync(key, value);
        return Ok($"Chave '{key}' com valor '{value}' armazenada no Redis.");
    }

    [HttpGet("get/{key}")]
    public async Task<IActionResult> GetValue(string key)
    {
        var value = await _redisService.GetValueAsync(key);

        if (value == null)
        {
            return NotFound($"Chave '{key}' não encontrada no Redis.");
        }

        return Ok($"Valor para '{key}': {value}");
    }
}
