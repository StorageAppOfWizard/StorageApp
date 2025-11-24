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

            return Result.Success(order.Select(o => o.ToDTO()).ToList());
        }

        public async Task<Result<OrderDTO>> GetByIdAsync(Guid id)
        {
            var order = await _unitOfWork.OrderRepository.GetById(id);
            if (order is null)
                return Result.NotFound("Order not found");

            return Result.Success(order.ToDTO());
        }

        public async Task<Result> RequestOrder(CreateOrderDTO dto)
        {
            var existingProduct = await _unitOfWork.ProductRepository.GetById(dto.ProductId);
            if (existingProduct is null)
                return Result.NotFound("Not Found Product, check if the product exist");

            if (existingProduct.Quantity < dto.Quantity)
                return Result.Error("There is not sufficient quantity for this order");
            if (dto.UserId is null)
                return Result.Error("Sign in for create a order");

            existingProduct.Quantity -= dto.Quantity;
            await _unitOfWork.OrderRepository.Create(dto.ToEntity());

            await _unitOfWork.CommitAsync();
            return Result.SuccessWithMessage("Order Created");
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            var order = await _unitOfWork.OrderRepository.GetById(id);

            if (order is null)
                return Result.NotFound("Order not exist");

            if (order.Status == OrderStatus.Approved)
                return Result.Error("You can't delete an order already approved");

            if (order.Status == OrderStatus.Pending)
            {
                await RestoreProductStock(order.ToDTO());
            }

            _unitOfWork.OrderRepository.Delete(order);
            await _unitOfWork.CommitAsync();

            return Result.SuccessWithMessage("Order deleted successfully");
        }

        public async Task<Result<OrderStatus>> CancelOrderAsync(Guid orderId, OrderStatus status)
        {
            var order = await _unitOfWork.OrderRepository.GetById(orderId);
            if (order is null)
                return Result.NotFound("Not Found Order");

            if (order.Status is not OrderStatus.Pending)
                return Result.Error("You can to cancel only pending order");

            await RestoreProductStock(order.ToDTO());

            return Result.Success(order.UpdateStatus(status));
        }

        public async Task<Result> UpdateOrderAsync(UpdateOrderDTO dto)
        {
            var order = await _unitOfWork.OrderRepository.GetById(dto.Id);
            if (order is null)
                return Result.NotFound("Order Not Found");

            if (order.Status == OrderStatus.Approved)
                return Result.Error("You can't update one order already approved");

            dto.ToEntity(order);

            await _unitOfWork.CommitAsync();
            return Result.SuccessWithMessage("Order Updated");
        }


        private async Task RestoreProductStock(OrderDTO order)
        {
            var product = await _unitOfWork.ProductRepository.GetById(order.ProductId);
            if (product is null) return;

            product.Quantity += order.Quantity;

        }

    }
}
