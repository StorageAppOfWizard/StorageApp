using StorageProject.Application.DTOs.Order;
using StorageProject.Application.DTOs.Product;
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
                Quantity = order.QuantityProduct,
                Status = order.Status,
                UserId = order.UserId,
            };
        }

        public static Order ToEntity(this OrderDTO dto)
        {
            return new Order
            {
                ProductId = dto.ProductId,
                UserId = dto.UserId,
                QuantityProduct = dto.Quantity

            };
        }

        public static Order ToEntity(this CreateOrderDTO dto)
        {
            return new Order
            {
                ProductId = dto.ProductId,
                UserId = dto.UserId,
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
