using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Middleware.WebAPI.Filters;

namespace Middleware.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class ValuesController : ControllerBase
{
    [HttpPost]
    [MyAuthorize]
    //[Log1]
    //[Log2]
    public IActionResult Method(User user)
    {
        var context = HttpContext;
        return Ok(new {Message = "API is working...", Note= "Something..."});
    }
}

public class User
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
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
