using Ardalis.Result;
using StorageProject.Domain.Entities.Enums;
using StorageProject.Domain.Entity;

namespace StorageProject.Application.Contracts
{
    public interface IOrderService
    {
        Task<Result<OrderStatus>> CancelOrderAsync(Guid orderId, OrderStatus status);
        Task<Result<Order>> RequestOrder(Order order);
        Task<Result> DeleteAsync(Guid id);
        Task<Result> CreateAsync(Order order);
        Task<Result<Order>> GetByIdAsync(Guid id);
        Task<Result<List<Order>>> GetAllAsync();

    }
}
