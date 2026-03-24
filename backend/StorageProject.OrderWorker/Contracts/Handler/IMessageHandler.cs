namespace StorageProject.OrderWorker.Contracts.Handler
{
    public interface IMessageHandler<TMessage>
    {
        Task HandleAsync(TMessage message, CancellationToken ct);
    }
}
