
namespace Middleware.WebAPI.Middlewares;

public sealed class LogMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {

        Log log = new(
            context.Request.HttpContext.Connection.RemoteIpAddress!.ToString(),
            context.Request.Method,
            context.Request.Path,
            context.Request.QueryString.Value ?? string.Empty,
            DateTime.Now);        

        await next(context);
    }
}


public sealed record Log(
    string IP,
    string MethodType,
    string EndPoint,
    string QueryString,
    DateTime Date);