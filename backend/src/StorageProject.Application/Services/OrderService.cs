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

        public async Task<Result> CreateOrderAsync(CreateOrderDTO dto)
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

        public async Task<Result> RejectOrderAsync(Guid orderId)
        {
            var order = await _unitOfWork.OrderRepository.GetById(orderId);
            if (order is null)
                return Result.NotFound("Not Found Order");

            if (order.Status is not OrderStatus.Pending)
                return Result.Error("You can to reject only pending order");
            
            order.UpdateStatus(OrderStatus.Reject);

            var restoreResult = await RestoreProductStock(order);
            
            if (!restoreResult.IsSuccess)
                    return restoreResult;

            _unitOfWork.OrderRepository.Update(order);
            await _unitOfWork.CommitAsync();
            return Result.SuccessWithMessage("Order Rejected");
        }
        
        public async Task<Result> ApproveOrderAsync(Guid orderId)
        {
            var order = await _unitOfWork.OrderRepository.GetById(orderId);
            if (order is null)
                return Result.NotFound("Not Found Order");

            if (order.Status is not OrderStatus.Pending)
                return Result.Error("You can to approve only pending order");
            
            order.UpdateStatus(OrderStatus.Approved);

            _unitOfWork.OrderRepository.Update(order);
            await _unitOfWork.CommitAsync();

            return Result.SuccessWithMessage("Order Approved");
        }
        
        public async Task<Result> DeleteOrderAsync(Guid id)
        {
            var order = await _unitOfWork.OrderRepository.GetById(id);

            if (order is null)
                return Result.NotFound("Order not exist");

            if (order.Status == OrderStatus.Approved)
                return Result.Error("You can't delete an order already approved");

            if (order.Status == OrderStatus.Pending)
            {
                var restoreResult  = await RestoreProductStock(order);
                if (!restoreResult.IsSuccess)
                    return restoreResult;
            }

            _unitOfWork.OrderRepository.Delete(order);
            await _unitOfWork.CommitAsync();

            return Result.SuccessWithMessage("Order deleted successfully");
        }

        public async Task<Result> UpdateOrderAsync(UpdateOrderDTO dto)
        {
            var order = await _unitOfWork.OrderRepository.GetById(dto.Id);
            if (order is null)
                return Result.NotFound("Order not found");

            var product = await _unitOfWork.ProductRepository.GetById(dto.ProductId);
            if (product is null)
                return Result.NotFound("Product not found");

            if (order.Status == OrderStatus.Approved)
                return Result.Error("You can't update an order that is already approved.");

            var oldStatus = order.Status;
            var newStatus = dto.Status;

            if (oldStatus == OrderStatus.Pending && newStatus == OrderStatus.Reject)
                product.Quantity += order.QuantityProduct;


            if (oldStatus == OrderStatus.Reject && newStatus == OrderStatus.Pending)
            {
                if (product.Quantity < dto.Quantity)
                    return Result.Error("Insufficient stock to reopen this order.");

                product.Quantity -= dto.Quantity;
            }

            dto.ToEntity(order);
            order.UpdateStatus(newStatus);

            _unitOfWork.ProductRepository.Update(product);
            _unitOfWork.OrderRepository.Update(order);
            await _unitOfWork.CommitAsync();

            return Result.SuccessWithMessage("Order updated successfully.");
        }


        private async Task<Result> RestoreProductStock(Order order)
        {
            var product  = await _unitOfWork.ProductRepository.GetById(order.ProductId);
            if (product is null)
                return Result.Error("Product not found - unable to restore stock");

            product.Quantity += order.QuantityProduct;
            return Result.Success();
        }

    }
}
