using Ardalis.Result;
using StorageProject.Application.DTOs.Order;
using StorageProject.Domain.Entity;

namespace StorageProject.Application.Contracts
{
    public interface IOrderStatusHandler
    {
        Result Handle(Order order, Product? product, UpdateOrderDTO? dto);
    }
}
