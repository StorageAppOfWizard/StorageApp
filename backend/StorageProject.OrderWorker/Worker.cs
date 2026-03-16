using StorageProject.OrderWorker.Contracts;

namespace StorageProject.OrderWorker
{
    public class Worker(ILogger<Worker> logger, IMessageConsumer messageConsumer) : BackgroundService
    {
        private readonly IMessageConsumer _messageConsumer = messageConsumer;
        private readonly ILogger<Worker> _logger;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _messageConsumer.ConsumerMessage();

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
