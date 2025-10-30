using Ardalis.Result;
using StorageProject.Domain.Entities.Enums;
using StorageProject.Domain.Entity;

namespace StorageProject.Application.Contracts
{
    public interface IOrderHandler
    {
        OrderStatus TargetStatus{ get;}
        public Task<Result<Order>> Handle(Order order, Product? product);
    }
}
