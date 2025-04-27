using RedisAPI.Commons;
using RedLockNet.SERedis.Configuration;
using RedLockNet.SERedis;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddRedisServices();


// Configuração do Redis
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost:6379"));

// Configuração do Redis Cache com IDistributedCache
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
});

// Adicionar RedLockFactory
builder.Services.AddSingleton<RedLockFactory>(sp =>
{
    var multiplexer = sp.GetRequiredService<IConnectionMultiplexer>();
    var endpoints = multiplexer.GetEndPoints();
    var connections = endpoints.Select(endpoint => multiplexer.GetServer(endpoint)).Select(server => new RedLockEndPoint
    {
        EndPoint = server.EndPoint
    }).ToList();

    return RedLockFactory.Create(new List<RedLockMultiplexer>
    {
        new RedLockMultiplexer(multiplexer)
    });
});

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
