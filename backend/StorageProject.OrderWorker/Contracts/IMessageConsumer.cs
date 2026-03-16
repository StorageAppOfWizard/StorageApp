namespace StorageProject.OrderWorker.Contracts
{
    public interface IMessageConsumer
    {
        public Task ConsumerMessage();
    }
}
