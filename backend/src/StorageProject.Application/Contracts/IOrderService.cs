using Ardalis.Result;
using StorageProject.Application.DTOs.Order;
using StorageProject.Domain.Entities.Enums;

namespace StorageProject.Application.Contracts
{
    public interface IOrderService
    {
        Task<Result<OrderStatus>> CancelOrderAsync(Guid orderId, OrderStatus status);
        Task<Result> RequestOrder(CreateOrderDTO dto);
        Task<Result> UpdateOrderAsync(UpdateOrderDTO dto);
        Task<Result> DeleteAsync(Guid id);
        Task<Result<OrderDTO>> GetByIdAsync(Guid id);
        Task<Result<List<OrderDTO>>> GetAllAsync();

    }
}
