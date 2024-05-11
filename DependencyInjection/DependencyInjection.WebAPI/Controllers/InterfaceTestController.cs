using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public sealed class InterfaceTestController(ICache cache) : ControllerBase
{
    //private readonly ICache _cache;
    //public InterfaceTestController(ICache cache)
    //{
    //    _cache = cache;
    //}
    [HttpGet]
    public IActionResult Get1()
    {

        ICache cache = new RedisCache();

        cache.CreateCache();

        return Ok();
    }

    [HttpGet]
    public IActionResult Get2()
    {
        cache.CreateCache();

        return Ok();
    }
}

public interface ICache
{
    void CreateCache();
}
public class MemoryCache : ICache
{
    public MemoryCache()
    {
        
    }
    public void CreateCache()
    {

    }
}

public class RedisCache : ICache
{
    public RedisCache()
    {
        
    }
    public void CreateCache()
    {

    }
}