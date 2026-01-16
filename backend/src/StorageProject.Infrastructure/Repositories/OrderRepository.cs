using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Order>> GetOrdersByUserId(int page, int pageQuantity, string userId, CancellationToken cancellationToken = default)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.Product)
                .Skip((page - 1) * pageQuantity)
                .Take(pageQuantity)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Order>> GetOrderWithIncludes(int page, int pageQuantity, CancellationToken cancellationToken = default)
        {
            return await _context.Orders
                .Include(o => o.Product)
                .Skip((page - 1) * pageQuantity)
                .Take(pageQuantity)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
