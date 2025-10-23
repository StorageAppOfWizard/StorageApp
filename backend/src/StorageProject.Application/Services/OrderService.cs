using Ardalis.Result;
using StorageProject.Application.Contracts;
using StorageProject.Application.DTOs.Order;
using StorageProject.Application.Mappers;
using StorageProject.Domain.Contracts;
using StorageProject.Domain.Entities.Enums;

namespace StorageProject.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<OrderDTO>>> GetAllAsync()
        {
            var order = await _unitOfWork.OrderRepository.GetAll();
            if (order is null)
                return Result.NoContent();

            
            return Result.Success(order.Select(o=>o.ToDTO()).ToList());
        }

        public async Task<Result<OrderDTO>> GetByIdAsync(Guid id)
        {
            var order = await _unitOfWork.OrderRepository.GetById(id);
            if (order is null)
                return Result.NoContent();

            return Result.Success(order.ToDTO());
        }

        public async Task<Result> CreateAsync(CreateOrderDTO order)
        {
            await _unitOfWork.OrderRepository.Create(order.ToEntity());
            await _unitOfWork.CommitAsync();
            return Result.Success();
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            var order = await _unitOfWork.OrderRepository.GetById(id);
            
            _unitOfWork.OrderRepository.Delete(order);

            return Result.Success();
        }

        public async Task<Result<OrderStatus>> CancelOrderAsync(Guid orderId, OrderStatus status)
        {
            var order = await _unitOfWork.OrderRepository.GetById(orderId);
            if (order is null)
                return Result.NotFound("Not Found Order");

            if (order.Status is not OrderStatus.Pending)
                return Result.Error("You can to cancel only pending order");

            return Result.Success(order.UpdateStatus(status));
        }

        public Task<Result<OrderDTO>> RequestOrder(OrderDTO order)
        {
            throw new NotImplementedException();
        }
    }
}
