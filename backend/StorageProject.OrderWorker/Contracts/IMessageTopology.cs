using RabbitMQ.Client;

namespace StorageProject.OrderWorker.Contracts
{
    public interface IMessageTopology
    {
        public Task ConfigureAsync(IChannel channel);
    }
}
