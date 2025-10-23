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
                ProductId = order.ProductId,
                Quantity = order.QuantityProduct,
                Status = order.Status,
                UserId = order.UserId,
            };
        }

        public static Order ToEntity(this OrderDTO order)
        {
            return new Order
            {
                ProductId = order.ProductId,
                UserId = order.UserId,
                QuantityProduct = order.Quantity

            };
        }

        public static Order ToEntity(this CreateOrderDTO order)
        {
            return new Order
            {
                ProductId = order.ProductId,
                UserId = order.UserId,
                QuantityProduct = order.Quantity
            };
        }
    }
}
