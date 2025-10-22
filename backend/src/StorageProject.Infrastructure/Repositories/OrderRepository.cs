using StorageProject.Domain.Contracts;
using StorageProject.Domain.Entity;
using StorageProject.Infrasctructure.Data;

namespace StorageProject.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public void CancelOrder(Order order)
        {
           
            _context.Orders.Update(order);
        }

        public void RequestOrder(Guid productId, int quantity, string UserId)
        {
            
        }

        Task IOrderRepository.RequestOrder(Guid productId, int quantity, string UserId)
        {
            throw new NotImplementedException();
        }
    }
}
