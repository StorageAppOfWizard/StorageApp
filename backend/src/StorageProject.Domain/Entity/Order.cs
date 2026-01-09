using StorageProject.Domain.Abstractions;
using StorageProject.Domain.Entities.Enums;

namespace StorageProject.Domain.Entity
{
    public class Order : EntityBase
    {
        public OrderStatus Status { get; private set; } = OrderStatus.Pending;
        public Guid ProductId { get; set; }
        public int QuantityProduct { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }



        public void UpdateStatus(OrderStatus status)
        {
            Status = status;
        }
    }


}
