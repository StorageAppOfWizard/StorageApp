using MassTransit;
using StorageProject.Domain.Contracts;

namespace StorageProject.Api
{
    public class MessagePublisher(IBus bus) : IMessagePublisher
    {
        public async Task SendMessage<T>(T message, CancellationToken cancellationToken = default)
        {
            await bus.Publish(message, cancellationToken);
        }
    }
}
