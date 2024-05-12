using MinimalAPI.Models;
using MinimalAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<User>();

var provider = builder.Services.BuildServiceProvider();

var userService = provider.GetService<IUserService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("api/GetAllUsers", async (IUserService userservice, User user, int age, string name) =>
{
    await userservice.CreateUserAsync();
    await Task.CompletedTask;
    // return "API is working...";
    return Results.Ok();
    return Results.BadRequest();
});

app.MapPost("api/GetAllUsers2", async (int age, string name) =>
{
    await userService.CreateUserAsync();
    await Task.CompletedTask;
    return "API is working...";
}).WithName("Test").WithOpenApi();

app.Run();
