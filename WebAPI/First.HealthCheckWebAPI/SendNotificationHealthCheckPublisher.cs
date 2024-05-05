using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace First.HealthCheckWebAPI;

public sealed class SendNotificationHealthCheckPublisher : IHealthCheckPublisher
{
    public async Task PublishAsync(HealthReport report, CancellationToken cancellationToken)
    {
        foreach (var item in report.Entries)
        {
            if(item.Value.Status != HealthStatus.Healthy)
            {
                //bana mail gönder
                //bana sms at
                //db çöktüğü ya da çalışmadığı süreleri not et
            }
        }

        await Task.CompletedTask;
    } 
}
