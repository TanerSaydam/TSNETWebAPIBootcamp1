
using System.Text.Json;

namespace Middleware.WebAPI.Middlewares;

public class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var response = new { Message = ex.Message };
            var responseString = JsonSerializer.Serialize(response);

            context.Response.StatusCode = 500;
            if (ex.GetType() == typeof(DivideByZeroException))
            {
                context.Response.StatusCode = 409;
            }

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(responseString);
        }
    }
}
