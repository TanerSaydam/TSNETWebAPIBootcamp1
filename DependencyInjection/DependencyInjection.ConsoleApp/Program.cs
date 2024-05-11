using Microsoft.Extensions.DependencyInjection;

ServiceCollection services = new();
services.AddScoped<RedisCache>(); //Dependency Injection

var provider = services.BuildServiceProvider(); 


var cache = provider.GetRequiredService<RedisCache>(); //inject



cache.CreateCache();


public class MemoryCache
{
    public void CreateCache()
    {
        Console.WriteLine("Memory cache is created");
    }
}

public class RedisCache
{
    public void CreateCache()
    {
        Console.WriteLine("Redis cache is created");
    }
}