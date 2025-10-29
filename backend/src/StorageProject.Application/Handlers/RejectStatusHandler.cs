using Ardalis.Result;
using StorageProject.Application.Contracts;
using StorageProject.Application.DTOs.Order;
using StorageProject.Domain.Entities.Enums;
using StorageProject.Domain.Entity;

namespace StorageProject.Application.Handlers
{
    internal class RejectStatusHandler : IOrderStatusHandler

    {
        public Result Handle(Order order, Product? product, UpdateOrderDTO? dto)
        {
            product.Quantity += order.QuantityProduct;
            order.UpdateStatus(OrderStatus.Reject);
            return Result.Success();
        }
    }
}
