using MassTransit;
using StorageProject.Application.DTOs.Messages;

namespace StorageProject.OrderWorker
{
    public class CurrentMessageConsumer (ILogger<CurrentMessageConsumer> logger) : IConsumer<OrderCreatedMessage>
    {
        public Task Consume(ConsumeContext<OrderCreatedMessage> context)
        {
            Console.WriteLine("{Consumer}: {Message}", nameof(OrderCreatedMessage), context.Message);
            
            return Task.CompletedTask;
        }
    }
}
