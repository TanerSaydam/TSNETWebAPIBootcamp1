using Microsoft.AspNetCore.Diagnostics;
using Middleware.WebAPI.Controllers;

namespace Middleware.WebAPI.Middlewares;

public sealed class MyExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var result = Result<string>.Failed(exception.Message).ToString();

        //var resultString = JsonSerializer.Serialize(result);

        httpContext.Response.StatusCode = 500;
        if (exception.GetType() == typeof(DivideByZeroException))
        {
            httpContext.Response.StatusCode = 409;
        }

        httpContext.Response.ContentType = "application/json";
        await httpContext.Response.WriteAsync(result);

        return true;

    }
}
