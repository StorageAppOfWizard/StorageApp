using StorageProject.OrderWorker.Contracts.Handler;

namespace StorageProject.OrderWorker.Handler
{
    public class QueueDispatcherHandler : IQueueDispatchHandler
    {
        private readonly IServiceProvider _provider;

        public QueueDispatcherHandler(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task DispatchAsync<TMessage>(TMessage message, CancellationToken cancellationToken)
        {
            var handler = _provider.GetService<IMessageHandler<TMessage>>();
            await handler.HandleAsync(message, cancellationToken);
        }
    }
}
