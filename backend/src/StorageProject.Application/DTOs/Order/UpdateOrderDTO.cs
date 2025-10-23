using StorageProject.Domain.Entities.Enums;

namespace StorageProject.Application.DTOs.Order
{
    public record UpdateOrderDTO : CreateOrderDTO
    {
        public Guid Id { get; set; }
        public OrderStatus Status{ get; set; }
    }
}
