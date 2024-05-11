using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public sealed class StaticClasslarController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        Service.CreateProduct();
        return Ok();
    }
}

public static class Service
{
    public static void CreateProduct()
    {
        //db ye kayıt işlemi

        var cache = ServiceTool.ServiceProvider!.GetRequiredService<ICache>();

        cache.CreateCache();
        //cache işlemi
    }
}

public static class ServiceTool
{
    public static IServiceProvider? ServiceProvider { get; set; }
    public static IServiceCollection CreateService(this IServiceCollection services)
    {
        ServiceProvider = services.BuildServiceProvider();
        return services;
    }
}
