using Ardalis.Result;
using StorageProject.Application.Contracts;
using StorageProject.Application.DTOs.Order;
using StorageProject.Domain.Entity;

namespace StorageProject.Application.Handlers
{
    internal class PendingStatusHandler : IOrderStatusHandler
    {
        public Result Handle(Order order, Product product, UpdateOrderDTO dto)
        {
            if (product.Quantity < dto.Quantity)
                return Result.Error("Insufficiente Stock");

            product.Quantity -= dto.Quantity;
            order.UpdateStatus(dto.Status);
            
            return Result.Success();    
        }


    }
}
