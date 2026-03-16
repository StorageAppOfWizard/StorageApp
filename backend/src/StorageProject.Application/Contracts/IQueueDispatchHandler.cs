using RabbitMQ.Client.Events;

namespace StorageProject.Application.Contracts
{
    public interface IQueueDispatchHandler
    {
        public Task DispatchHandler(string message, BasicDeliverEventArgs eventArgs);
    }
}
