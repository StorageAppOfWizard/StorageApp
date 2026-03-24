namespace StorageProject.OrderWorker.Contracts.Consumer
{
    public interface IMessageConsumer
    {
        public Task ConsumerMessage(CancellationToken cancellationToken);
    }
}
