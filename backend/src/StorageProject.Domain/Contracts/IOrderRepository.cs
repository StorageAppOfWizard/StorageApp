using StorageProject.Domain.Entity;

namespace StorageProject.Domain.Contracts
{
    public interface IOrderRepository : IRepository<Order>
    {
        public Task<IEnumerable<Order>> GetOrdersByUserId(int page, int pageQuantity, string userId, CancellationToken cancellationToken = default);
        public Task<IEnumerable<Order>> GetOrderWithIncludes( CancellationToken cancellationToken = default);


    }
}
