using Ardalis.Result;
using Microsoft.AspNetCore.Http;
using StorageProject.Application.Contracts;
using StorageProject.Application.DTOs.Order;
using StorageProject.Application.Mappers;
using StorageProject.Domain.Contracts;
using StorageProject.Domain.Entities.Enums;
using System.Security.Claims;

namespace StorageProject.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEnumerable<IOrderHandler> _orderHandler;
        private readonly IHttpContextAccessor _context;


        public OrderService(IUnitOfWork unitOfWork, IEnumerable<IOrderHandler> orderHandler, IHttpContextAccessor context)
        {
            _unitOfWork = unitOfWork;
            _orderHandler = orderHandler;
            _context = context;
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
            var userId = _context.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId is null) return Result.Forbidden();

            var orders = await _unitOfWork.OrderRepository.GetOrdersByUserId(userId);

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
            
            var userId = _context.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

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

            var handler = _orderHandler
                .FirstOrDefault(x => x.TargetStatus == OrderStatus.Reject);

            if (handler is null)
                return Result.Error($"There's not handler for status ${order.Status}");

            await handler.Handle(order,product);

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

            await handler.Handle(order,null);

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

    }
}
