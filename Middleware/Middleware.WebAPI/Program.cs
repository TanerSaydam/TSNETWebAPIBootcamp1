using Middleware.WebAPI.Controllers;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddScoped<Middlewares>();
builder.Services.AddTaner();
builder.Services.AddCors();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.UseHealthChecks();

app.UseCors();

app.UseTaner();

//app.Use(async (context, next) =>
//{
//    context.Response.StatusCode = 409;
//    //await next.Invoke();
//    await next();
    
//});

app.Run();
