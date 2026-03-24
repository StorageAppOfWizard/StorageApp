using RabbitMQ.Client;

namespace StorageProject.OrderWorker.Contracts.Configuration
{
    public interface IMessageConfiguration
    {
        public Task ConfigureAsync(IChannel channel);
    }
}
