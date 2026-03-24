namespace StorageProject.OrderWorker.Contracts.Handler
{
    public interface IQueueDispatchHandler
    {
        public Task DispatchAsync<TMessage>(TMessage message, CancellationToken cancellationToken);
    }
}
