using StorageProject.Domain.Entities.Enums;

namespace StorageProject.Application.DTOs.Order
{
    public record OrderDTO
    {
        public Guid Id { get; set; }
        public Guid ProductId{ get; init; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public OrderStatus Status{ get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime CreationDate { get; set; }


    }
}
