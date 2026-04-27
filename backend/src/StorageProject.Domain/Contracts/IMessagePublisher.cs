namespace StorageProject.Domain.Contracts
{
    public interface IMessagePublisher
    {
        public Task SendMessage<T>(T message, CancellationToken cancellationToken = default);
    }
}
