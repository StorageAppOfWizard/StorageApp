using RabbitMQ.Client;

namespace StorageProject.OrderWorker.Contracts
{
    public interface IMessageConfiguration
    {
        public Task ConfigureAsync(IChannel channel);
    }
}
