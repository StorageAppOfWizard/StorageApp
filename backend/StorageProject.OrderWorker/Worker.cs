using MassTransit;

namespace StorageProject.OrderWorker
{
    public class Worker() : BackgroundService
    {
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

           await Task.Delay(1000, stoppingToken);
        }
    }
}
