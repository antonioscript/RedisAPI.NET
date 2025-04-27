using RedisAPI.Contracts;
using StackExchange.Redis;

namespace RedisAPI.Implements;
public class RedisLockService : IRedisLockService
{
    private readonly IConnectionMultiplexer _redis;

    public RedisLockService(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    public async Task<bool> ExecuteWithLockAsync(string lockKey, TimeSpan expiry, Func<Task> action)
    {
        var db = _redis.GetDatabase();

        var redisLockKey = $"lock:{lockKey}";
        var lockValue = Guid.NewGuid().ToString(); //Aqui é criado como se fosse um "Id" pra essa operação

        // Tentando adquirir o lock
        bool acquired = await db.StringSetAsync(redisLockKey, lockValue, expiry, when: When.NotExists);

        if (!acquired)
        {
            // Não conseguiu pegar o lock
            return false;
        }

        try
        {
            // Executa a ação enquanto tiver o lock
            await action.Invoke();
        }
        finally
        {
            // Libera o lock (só se ainda for o dono)
            var value = await db.StringGetAsync(redisLockKey);

            if (value == lockValue)
            {
                await db.KeyDeleteAsync(redisLockKey);
            }
        }

        return true;
    }


    /*
 1. Como testar a concorrência ao tentar adquirir o Lock:
    - Vá ao banco e crie manualmente uma chave chamada: lock:critical-task com um GUID aleatório.
    - Quando a aplicação bater no Redis e tentar criar novamente a mesma chave, ela não vai conseguir
      (porque o Redis impede sobrescrita com When.NotExists) e a variável "acquired" será marcada como false.
    - Resultado esperado: O endpoint retornará 409 Conflict.

 2. Como testar a falha ao tentar liberar o Lock (não é mais o dono):
    - Dispare uma requisição normalmente para o endpoint que adquire o lock.
    - Durante o tempo de processamento (enquanto o método action ainda está executando),
      altere manualmente o valor da chave no Redis ou delete a chave.
    - Quando o código tentar liberar o lock no finally, ele verá que o valor atual não é mais o seu
      (o lockValue mudou ou a chave sumiu).
    - Resultado esperado: O processo detecta que perdeu a posse do lock e não tenta apagar.
    - Como ele não apagou a chave: lock:critical-task vai continuar lá e nenhum outro processo vai 
      conseguir criar uma outra chave com esse nome. Sò vai conseguir quando o tempo expirar e ela se auto deletar
*/

}