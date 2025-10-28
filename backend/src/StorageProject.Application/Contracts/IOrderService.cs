using Ardalis.Result;
using StorageProject.Application.DTOs.Order;

namespace StorageProject.Application.Contracts
{
    public interface IOrderService
    {
        Task<Result> RejectOrderAsync(Guid orderId);
        Task<Result> ApproveOrderAsync(Guid orderId);
        Task<Result> CreateOrderAsync(CreateOrderDTO dto);
        Task<Result> UpdateOrderAsync(UpdateOrderDTO dto);
        Task<Result> DeleteOrderAsync(Guid id);
        Task<Result<OrderDTO>> GetByIdAsync(Guid id);
        Task<Result<List<OrderDTO>>> GetAllAsync();

    }
}
