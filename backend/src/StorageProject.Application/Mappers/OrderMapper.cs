using StorageProject.Application.DTOs.Order;
using StorageProject.Domain.Entity;

namespace StorageProject.Application.Mappers
{
    public static class OrderMapper
    {
        public static OrderDTO ToDTO(this Order order)
        {
            return new OrderDTO
            {
                Id = order.Id,
                ProductId = order.ProductId, 
                ProductName = order.Product.Name ?? string.Empty,
                Quantity = order.QuantityProduct,
                Status = order.Status,
                UserId = order.UserId,
                UserName = order.UserName,
                CreationDate = order.CreationDate

            };
        }

        public static Order ToEntity(this OrderDTO dto)
        {
            return new Order
            {
                ProductId = dto.ProductId,
                UserId = dto.UserId,
                UserName = dto.UserName,
                QuantityProduct = dto.Quantity

            };
        }

        public static Order ToEntity(this CreateOrderDTO dto, string userId, string userName)
        {
            return new Order
            {
                ProductId = dto.ProductId,
                UserId = userId,
                UserName = userName,
                QuantityProduct = dto.Quantity,

            };
        }

        public static void ToEntity(this UpdateOrderDTO dto, Order order)
        {

            order.ProductId = dto.ProductId;
            order.QuantityProduct = dto.Quantity;
        }
    }
}
