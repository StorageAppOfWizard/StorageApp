using Ardalis.Result;
using StorageProject.Application.Contracts;
using StorageProject.Domain.Entities.Enums;
using StorageProject.Domain.Entity;

namespace StorageProject.Application.Handlers
{
    public class ApprovedHandler : IOrderHandler
    {
        public OrderStatus TargetStatus => OrderStatus.Approved;

        public async Task<Result<Order>> Handle(Order order, Product product)
        {
            if (order.Status != OrderStatus.Pending)
            {
                return Result.Error("Just pending order can be approved");
            }
            order.UpdateStatus(OrderStatus.Approved);

            return Result.Success(order);
        }
    }
}
