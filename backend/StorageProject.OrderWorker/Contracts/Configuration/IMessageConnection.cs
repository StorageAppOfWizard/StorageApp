using RabbitMQ.Client;

namespace StorageProject.OrderWorker.Contracts.Configuration
{
    public interface IMessageConnection
    {
        IConnection GetConnection();
    }
}
