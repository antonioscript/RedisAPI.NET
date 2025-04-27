# RedisAPI.NET
API built in .NET showing the functionalities of Redis as Cache and as Globak Lock

## Redis Installation via Docker

### Pull the Redis image

```bash
docker pull redis:latest
```

### Run the Redis Container
```bash
docker run -d --name redis-server1 -p 6379:6379 redis:latest
```

### Test if Redis is running
```bash
docker exec redis-server1 redis-cli ping
```
Se retornar PONG, o Redis está ativo.


## Using Redis interactively in Visual Studio Code
**Entensão**: https://redis.io/docs/latest/develop/tools/redis-for-vscode/

### Simple Query Examples

#### 1. Consulta - HGET + {key} + {field}
```bash
> HGET AnaMaria salario
"5000"
```

#### 2. Consulta - HGETALL + {key}
```bash
> HGETALL AntonioRocha

1) "nome"
2) "Antonio"
3) "sobrenome"
4) "Rocha"
5) "cidade"
6) "Jundiai"
7) "uf"
8) "sp"
9) "salario"
10) "5000"
```
![image](https://github.com/user-attachments/assets/103348c4-642c-44a4-a2b6-69139a4566fa)

## API Functionalities
![image](https://github.com/user-attachments/assets/b6982a95-2ac0-4e44-b706-2af2fc0cb840)


### Redis
Basic use of Redis to create a key-value pair and retrieve the value associated with a key.

### RedisCache
Using Redis as a distributed cache, creating and retrieving key-value content through Microsoft's native IDistributedCache interface.

### RedLock
Using Redis for distributed locking, both with the native approach and using the RedLock library for .NET.

### NuGet Packages
https://www.nuget.org/packages/stackexchange.redis
https://www.nuget.org/packages/microsoft.extensions.caching.stackexchangeredis
https://www.nuget.org/packages/RedLock.net

