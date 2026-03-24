using StorageProject.Application.DTOs.Messages;
using StorageProject.Domain.Contracts;
using StorageProject.OrderWorker.Contracts.Handler;

namespace StorageProject.OrderWorker.Handler
{
    public class OrderCreatedHandler (IUnitOfWork unitOfWork): IMessageHandler<OrderCreatedMessage>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;


        public async Task HandleAsync(OrderCreatedMessage message, CancellationToken ct)
        {

            var product = await _unitOfWork.ProductRepository.GetById(message.ProductId);

            product.Quantity -= message.QuantityProduct;


            await _unitOfWork.CommitAsync();

        }
    }
}
