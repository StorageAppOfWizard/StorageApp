using MassTransit;
using StorageApp.Orders.Domain.Entity;

namespace StorageProject.Api
{
    public class CurrentMessageConsumer(ILogger<CurrentMessageConsumer> logger) : IConsumer<OrderMessage>
    {
        public async Task Consume(ConsumeContext<OrderMessage> context)
        {
            logger.LogInformation("{Message}", context.Message);

            await Task.Delay(5000);
        }
    }
}
