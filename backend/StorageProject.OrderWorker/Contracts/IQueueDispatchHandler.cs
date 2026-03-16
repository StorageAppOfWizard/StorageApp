using RabbitMQ.Client.Events;

namespace StorageProject.OrderWorker.Contracts
{
    public interface IQueueDispatchHandler
    {
        public Task DispatchHandler(string message, BasicDeliverEventArgs eventArgs);
    }
}
