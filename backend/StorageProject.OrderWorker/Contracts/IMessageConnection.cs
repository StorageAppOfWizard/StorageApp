using RabbitMQ.Client;

namespace StorageProject.OrderWorker.Contracts
{
    public interface IMessageConnection
    {
        IConnection GetConnection();
    }
}
