using Ardalis.Result;
using StorageProject.Application.DTOs.Order;
using StorageProject.Domain.Entities.Enums;

namespace StorageProject.Application.Contracts
{
    public interface IOrderService
    {
        Task<Result<OrderStatus>> CancelOrderAsync(Guid orderId, OrderStatus status);
        Task<Result<OrderDTO>> RequestOrder(OrderDTO order);
        Task<Result> DeleteAsync(Guid id);
        Task<Result> CreateAsync(CreateOrderDTO order);
        Task<Result<OrderDTO>> GetByIdAsync(Guid id);
        Task<Result<List<OrderDTO>>> GetAllAsync();

    }
}
