using MassTransit;
using Microsoft.Extensions.Logging;
using StorageApp.Shared;
using StorageProject.Domain.Contracts;
using StorageProject.Domain.Entity.Messages.Storage;

namespace StorageProject.Application.Handler
{
    public class OrderCreatedMessageHandler(ILogger<OrderCreatedMessageHandler> logger, IUnitOfWork unitOfWork) : IConsumer<OrderCreatedMessage>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task Consume(ConsumeContext<OrderCreatedMessage> context)
        {
            var existingProduct = await _unitOfWork.ProductRepository.GetById(context.Message.ProductId);
            if (existingProduct is null)
            {
                await context.Publish(new StockRejectedMessage {
                    Id = Guid.NewGuid(),
                    StockAvailable = false,
                    OrderId = context.Message.Id
                });

                logger.LogInformation("Order Rejected by Stock");
                return;
            }

            if (existingProduct.Quantity < context.Message.QuantityProduct)
            {
                await context.Publish(new StockRejectedMessage { Id = Guid.NewGuid(), StockAvailable = false });
                logger.LogInformation("Order Rejected by Stock");

                existingProduct.Quantity -= context.Message.QuantityProduct;
                return;
            }


            await _unitOfWork.CommitAsync();
            logger.LogInformation("Stock Reserved Product's Order");
        }
    }
}
