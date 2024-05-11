using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Middleware.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class ValuesController : ControllerBase
{
    [HttpGet]
    public IActionResult Method()
    {
        var context = HttpContext;
        return Ok();
    }
}


public static class Extensions
{
    public static IApplicationBuilder UseTaner(this WebApplication app)
    {

        app.UseMiddleware<Middlewares>();
        //app.Use((context, next) =>
        //{
        //    return next(context);
        //});

        return app;
    }

    public static IServiceCollection AddTaner(this IServiceCollection services)
    {
        services.AddScoped<Middlewares>();

        return services;
    }
}

public class Middlewares : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        context.Response.StatusCode = 501;
        await next(context);
    }
}
