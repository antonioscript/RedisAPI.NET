# RedisAPI.NET
API built in .NET showing the functionalities of Redis as Cache and as Globak Lock

## Instalação do Redis via Docker

### Baixar imagem do Redis

```bash
docker pull redis:latest
```

### Executar o Container Redis
```bash
docker run -d --name redis-server1 -p 6379:6379 redis:latest
```

### Testar se o Redis está funcionando
```bash
docker exec redis-server1 redis-cli ping
```
Se retornar PONG, o Redis está ativo.


## Redis de Forma Interativa no Visual Studio Code
**Entensão**: https://redis.io/docs/latest/develop/tools/redis-for-vscode/

### Consulta Simples

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

## Funcionalidades da API
![image](https://github.com/user-attachments/assets/b6982a95-2ac0-4e44-b706-2af2fc0cb840)


### Redis
Uso simples do Redis, onde é possível criar uma chave e valor, e também recuperar o valor dessa chave

### RedisCache
Uso do Redis como cache distribuído, onde é possível criar um conteúdo do tipo valor-chave e recuperar as informações de cache, usando a interface nativa da microsoft: IDistributedCache 

### RedLock
Uso do Redis como bloqueio, usando a forma nativa e usando a biblioteca para .NET

### Nugets Packages
https://www.nuget.org/packages/stackexchange.redis
https://www.nuget.org/packages/microsoft.extensions.caching.stackexchangeredis
https://www.nuget.org/packages/RedLock.net

