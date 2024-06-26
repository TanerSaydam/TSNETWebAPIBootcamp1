using DependencyInjection.WebAPI.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<Product>();
builder.Services.AddScoped<Test>();

builder.Services.AddScoped<ICache, RedisCache>();

//builder.Services.AddTransient<Product>(); //=> Dependency Injection
//builder.Services.AddSingleton<Product>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.CreateService();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
