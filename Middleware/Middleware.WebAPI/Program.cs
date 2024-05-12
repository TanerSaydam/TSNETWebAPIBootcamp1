using Middleware.WebAPI.Controllers;
using Middleware.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddScoped<Middlewares>();
builder.Services.AddTaner();
builder.Services.AddCors();

builder.Services.AddExceptionHandler<MyExceptionHandler>().AddProblemDetails();

//builder.Services.AddScoped<LogFilter>();

builder.Services.AddControllers(options =>
{
    //options.Filters.AddService<LogFilter>();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<LogMiddleware>();
builder.Services.AddTransient<ExceptionMiddleware>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler();

//app.UseHealthChecks();

app.UseCors();

//app.Use(async (context, next) =>
//{
//    await next();
//});

//app.UseTaner();


app.UseMiddleware<LogMiddleware>();

//app.Use(async (context, next) =>
//{
//    context.Response.StatusCode = 409;
//    //await next.Invoke();
//    await next();

//});

//app.UseMiddleware<ExceptionMiddleware>();
//app.UseMyExceptionHandler();

//app.Use(async (context, next) =>
//{
//    try
//    {
//        await next(context);
//    }
//    catch (Exception ex)
//    {
//        var response = new { Message = ex.Message };
//        var responseString = JsonSerializer.Serialize(response);

//        context.Response.StatusCode = 500;
//        if (ex.GetType() == typeof(DivideByZeroException))
//        {
//            context.Response.StatusCode = 409;
//        }

//        context.Response.ContentType = "application/json";
//        await context.Response.WriteAsync(responseString);
//    }
//});

app.Run();
