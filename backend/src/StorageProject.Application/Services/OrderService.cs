using Ardalis.Result;
using StorageProject.Application.Contracts;
using StorageProject.Application.DTOs.Order;
using StorageProject.Application.Mappers;
using StorageProject.Domain.Contracts;
using StorageProject.Domain.Entities.Enums;
using StorageProject.Domain.Entity;

namespace StorageProject.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEnumerable<IOrderHandler> _orderHandler;
        private readonly IUserContextAuth _userContextAuth;


        public OrderService(IUnitOfWork unitOfWork, IEnumerable<IOrderHandler> orderHandler, IUserContextAuth userContextAuth)
        {
            _unitOfWork = unitOfWork;
            _orderHandler = orderHandler;
            _userContextAuth = userContextAuth;
        }

        public async Task<Result<List<OrderDTO>>> GetAllAsync()
        {
            var order = await _unitOfWork.OrderRepository.GetAll();
            if (order is null)
                return Result.Success();

            return Result.Success(order.Select(o => o.ToDTO()).ToList());
        }

        public async Task<Result<OrderDTO>> GetByIdAsync(Guid id)
        {
            var order = await _unitOfWork.OrderRepository.GetById(id);
            if (order is null)
                return Result.NotFound("Order not found");

            return Result.Success(order.ToDTO());
        }

        public async Task<Result<List<OrderDTO>>> GetOrdersByUserIdAsync()
        {

            if (_userContextAuth.IsAuthenticated is false) return Result.Forbidden();

            var orders = await _unitOfWork.OrderRepository.GetOrdersByUserId(_userContextAuth.UserId);

            if (orders is null || !orders.Any())
                return Result.Success();

            return Result.Success(orders.Select(o => o.ToDTO()).ToList());
        }

        public async Task<Result> CreateOrderAsync(CreateOrderDTO dto)
        {
            
            var existingProduct = await _unitOfWork.ProductRepository.GetById(dto.ProductId);
            if (existingProduct is null)
                return Result.NotFound("Not Found Product, check if the product exist");

            if (existingProduct.Quantity < dto.Quantity)
                return Result.Error("There is not sufficient quantity for this order");
            
            var userId = _userContextAuth.UserId;

            if (userId is null)
                return Result.Unauthorized("Sign in for create a order");

            existingProduct.Quantity -= dto.Quantity;
            await _unitOfWork.OrderRepository.Create(dto.ToEntity(userId));

            await _unitOfWork.CommitAsync();
            return Result.SuccessWithMessage("Order Created");
        }

        public async Task<Result> RejectOrderAsync(Guid orderId)
        {
            var order = await _unitOfWork.OrderRepository.GetById(orderId);
            var product = await _unitOfWork.ProductRepository.GetById(order.ProductId);

            if (order is null)
                return Result.NotFound("Not Found Order");

            var handler = _orderHandler.FirstOrDefault(x => x.TargetStatus == OrderStatus.Reject);

            if (handler is null)
                return Result.Error($"There's not handler for status ${order.Status}");

            var result = await handler.Handle(order,product);

            if (!result.IsSuccess) return Result.Error(result.Errors.FirstOrDefault());

            _unitOfWork.OrderRepository.Update(order);
            await _unitOfWork.CommitAsync();
            return Result.SuccessWithMessage("Order Rejected");
        }

        public async Task<Result> ApproveOrderAsync(Guid orderId)
        {
            var order = await _unitOfWork.OrderRepository.GetById(orderId);

            if (order is null)
                return Result.NotFound("Not Found Order");

            var handler = _orderHandler
                 .FirstOrDefault(x => x.TargetStatus == OrderStatus.Approved);

            if (handler is null)
                return Result.Error($"There's not handler for status ${order.Status}");

            var result = await handler.Handle(order,null);

            if (!result.IsSuccess) return Result.Error(result.Errors.FirstOrDefault());

            _unitOfWork.OrderRepository.Update(order);
            await _unitOfWork.CommitAsync();

            return Result.SuccessWithMessage("Order Approved");
        }

        public async Task<Result> DeleteOrderAsync(Guid id)
        {
            var order = await _unitOfWork.OrderRepository.GetById(id);
            var product = await _unitOfWork.ProductRepository.GetById(order.ProductId);

            if (order is null)
                return Result.NotFound("Order not exist");

            var handler = _orderHandler
                .FirstOrDefault(x => x.TargetStatus == OrderStatus.Reject);

            if (handler is null)
                return Result.Error($"There's not handler for status ${order.Status}");

            await handler.Handle(order,product);

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
            order.UpdateStatus(status);
            return Result.Success();
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
