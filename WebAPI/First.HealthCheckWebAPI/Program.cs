using First.HealthCheckWebAPI;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHealthChecksUI(options =>
{
    options.AddHealthCheckEndpoint("Health Check API", "https://testapi.ecnorow.com/healthcheck");
})
    .AddInMemoryStorage();

builder.Services.Configure<HealthCheckPublisherOptions>(options =>
{
    options.Delay = TimeSpan.FromSeconds(15);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IHealthCheckPublisher, SendNotificationHealthCheckPublisher>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecksUI(options => options.UIPath = "/dashboard");

app.Run();
