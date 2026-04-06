using MassTransit;
using StorageProject.Application.DTOs.Messages;

namespace StorageProject.OrderWorker
{
    public class CurrentMessageConsumer (ILogger<CurrentMessageConsumer> logger) : IConsumer<OrderMessage>
    {
        public Task Consume(ConsumeContext<OrderMessage> context)
        {
            Console.WriteLine("{Consumer}: {Message}", nameof(OrderMessage), context.Message);
            
            return Task.CompletedTask;
        }
    }
}
