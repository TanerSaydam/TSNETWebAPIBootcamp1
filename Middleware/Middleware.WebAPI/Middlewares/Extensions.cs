namespace Middleware.WebAPI.Middlewares;

public static class Extensions
{
    public static IApplicationBuilder UseMyExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();

        return app;
    }
}
