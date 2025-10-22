using StorageProject.Domain.Entity;

namespace StorageProject.Domain.Contracts
{
    public interface IOrderRepository : IRepository<Order>
    {
        public Task RequestOrder(Guid productId, int quantity, string UserId);
        public void CancelOrder(Order order);
    }
}
