using RabbitMQ.Client;

namespace StorageProject.Application.Contracts
{
    public interface IMessageConnection
    {
        IConnection GetConnection();
    }
}
