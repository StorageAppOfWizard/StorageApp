using RabbitMQ.Client;

namespace StorageProject.Application.Contracts
{
    public interface IMessageTopology
    {
        public Task ConfigureAsync(IChannel channel);
    }
}
