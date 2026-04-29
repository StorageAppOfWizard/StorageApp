using MassTransit;
using StorageApp.Shared.Message.Order;

namespace StorageProject.Api
{
    public class CurrentMessageConsumer(ILogger<CurrentMessageConsumer> logger) : IConsumer<OrderCreatedMessage>
    {
        public async Task Consume(ConsumeContext<OrderCreatedMessage> context)
        {
            logger.LogInformation("{Message}", context.Message);

            await Task.Delay(5000);
        }
    }
}
