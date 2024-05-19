using Microsoft.AspNetCore.Authorization;

namespace MinimalAPI2.WebAPI.Enpoints;

public static class TodoEnpoints
{
    public static void UseTodo1EndPoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/get", () => "This is a get method").WithTags("Todos1");

        app.MapPut("/put", () => "This is a put method").WithTags("Todos1");
        app.MapDelete("/delete", () => "This is a delete method").WithTags("Todos1");

        app.MapGet("/get3", () =>
        {
            //operations
            return "This is extend method";
        }).WithTags("Todos1");

        app.MapGet("/get5", () =>
        {
            return Results.Ok(new { Message = "This is a results ok method" });
        }).WithTags("Todos1");

        app.MapGet("/get6", (IConfiguration configuration, int a) =>
        {
            return "This is DI request";
        }).WithTags("Todos1");
    }

    public static void UseTodo2EndPoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/post", () => "This is a post method").WithTags("Todos2");

        var handler = () => "This is a handler";
        app.MapGet("/get2", handler).WithTags("Todos2");

        app.MapGet("/get4", async (CancellationToken cancellationToken) =>
        {
            await Task.CompletedTask;
            return "This is a async method";
        }).WithTags("Todos2");

        app.MapGet("/get7",
            [Authorize] () =>
            {
                return "This is a adding attribute method";
            }).WithTags("Todos2");
    }
}
