using DefaultCorsPolicyNugetPackage;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

//Cross-origin resource sharing (CORS)
builder.Services.AddHealthChecks();
builder.Services.AddHealthChecksUI(options =>
{
    options.AddHealthCheckEndpoint("Healthcheck API", "/healthcheck");
}).AddInMemoryStorage();

builder.Services.AddRateLimiter(configure =>
{
    configure.AddFixedWindowLimiter("fixed", options =>
    {
        options.PermitLimit = 100; //kabul edilecek istek sayýsý
        options.Window = TimeSpan.FromSeconds(1); //kaç saniyede bir
        options.QueueLimit = 100;
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });

    configure.AddSlidingWindowLimiter("sliding", options =>
    {
        options.PermitLimit = 100; //kabul edilecek istek sayýsý
        options.Window = TimeSpan.FromSeconds(1); //kaç saniyede bir
        options.QueueLimit = 100;
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.SegmentsPerWindow = 10;
    });
});

builder.Services.AddDefaultCors();

builder.Services.AddCors(action =>
{
    action.AddPolicy("First", policy =>
    {
        policy
        .WithOrigins("https://www.tanersaydam.net", "http://127.0.0.1:5500")//web sitelerine izin verir
        .WithMethods("GET")
        .WithHeaders("SecretKey")
        ;
    });

    action.AddPolicy("Second", policy =>
    {
        policy
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });

    action.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://www.tanersaydam.net"); //web sitelerine izin verir
    });
});

builder.Services.AddHttpContextAccessor();
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

app.UseHealthChecks("/healthcheck", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
    ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
    }
});

app.UseRateLimiter();

app.UseCors("First");

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers().RequireRateLimiting("fixed");

app.MapHealthChecksUI(options => options.UIPath = "/dashboard");

app.Run();
