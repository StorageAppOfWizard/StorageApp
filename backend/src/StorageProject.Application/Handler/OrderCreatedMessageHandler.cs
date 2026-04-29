using MassTransit;
using Microsoft.Extensions.Logging;
using StorageApp.Shared.Message.Order;
using StorageApp.Shared.Message.Storage;
using StorageProject.Domain.Contracts;
namespace StorageProject.Application.Handler
{
    public class OrderCreatedMessageHandler(
        ILogger<OrderCreatedMessageHandler> logger,
        IUnitOfWork unitOfWork,
        IMessagePublisher messagePublisher) : IConsumer<OrderCreatedMessage>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMessagePublisher _messagePublisher = messagePublisher;
        public async Task Consume(ConsumeContext<OrderCreatedMessage> context)
        {
            var existingProduct = await _unitOfWork.ProductRepository.GetById(context.Message.ProductId);
            if (existingProduct is null)
            {
                await context.Publish(new StorageRejectMessage
                {
                    Id = Guid.NewGuid(),
                    StockAvailable = false,
                    OrderId = context.Message.Id
                });

                logger.LogInformation("Order Rejected by Stock");
                return;
            }

            if (existingProduct.Quantity < context.Message.QuantityProduct)
            {
                await context.Publish(new StorageRejectMessage
                {
                    Id = Guid.NewGuid(),
                    StockAvailable = false,
                    OrderId = context.Message.Id
                });

                logger.LogInformation("Order Rejected by Stock");

                return;
            }


            existingProduct.Quantity -= context.Message.QuantityProduct;
            await _messagePublisher.SendMessage(new StockAcceptedMessage
            {
                Id = Guid.NewGuid(),
                StockAvailable = true,
                OrderId = context.Message.Id
            });


            await _unitOfWork.CommitAsync();
            logger.LogInformation("Stock Reserved Product's Order");
        }
    }
}
