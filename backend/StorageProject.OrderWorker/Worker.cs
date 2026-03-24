using StorageProject.OrderWorker.Contracts.Consumer;

namespace StorageProject.OrderWorker
{
    public class Worker(IMessageConsumer messageConsumer) : BackgroundService
    {
        private readonly IMessageConsumer _messageConsumer = messageConsumer;


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _messageConsumer.ConsumerMessage(stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
