using StorageProject.Domain.Entity;

namespace StorageProject.Domain.Contracts
{
    public interface IOrderRepository : IRepository<Order>
    {
        public void CancelOrder(Order order);
        public Task<IEnumerable<Order>> GetOrdersByUserId(string userId);
    }
}
