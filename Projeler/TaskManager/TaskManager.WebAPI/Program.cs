using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(action =>
{
    action.AddPolicy("PolicyFirst", policy =>
    {
        policy
            .WithMethods("GET", "POST", "PUT", "DELETE")
            .WithHeaders("Authorization")
            .WithOrigins("https://www.tspersonel.com", "http://127.0.0.1:5500");
    });
});

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("fixed", configure =>
    {
        configure.PermitLimit = 100;
        configure.Window = TimeSpan.FromSeconds(1); //1 saniyede 100 istek kabul et
        configure.QueueLimit = 100; // +100 de kuyruða al
        configure.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });
});

builder.Services.AddHealthChecks()
    .AddCheck("self", ()=> HealthCheckResult.Healthy());

//builder.Services.AddHealthChecksUI();

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

app.UseCors("PolicyFirst");

app.UseStaticFiles();

app.UseHealthChecks("/healthcheck", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseAuthorization();

app.MapControllers().RequireRateLimiting("fixed");

app.Run();
