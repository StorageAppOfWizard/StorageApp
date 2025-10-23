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



        public OrderStatus UpdateStatus(OrderStatus status)
        {
            if (Status is not OrderStatus.Pending)
                throw new InvalidOperationException("You can to cancel only pending order ");

            return Status = status;
        }
    }


}
