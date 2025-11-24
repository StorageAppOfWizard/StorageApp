using Ardalis.Result;
using StorageProject.Application.Contracts;
using StorageProject.Application.Extensions;
using StorageProject.Domain.Entities.Enums;
using StorageProject.Domain.Entity;

namespace StorageProject.Application.Handlers
{
    public class RejectHandler : IOrderHandler

    {
        public OrderStatus TargetStatus => OrderStatus.Reject;

        public async Task<Result<Order>> Handle(Order order, Product product)
        {

            if (order.Status != OrderStatus.Pending)
                return Result.Error("Just pending order can be rejected");

            await order.RestoreProductStock(product);
            order.UpdateStatus(OrderStatus.Reject);

            return Result.Success(order);
        }
    }
}
