using Microsoft.EntityFrameworkCore;
using StorageProject.Domain.Contracts;
using StorageProject.Domain.Entity;
using StorageProject.Infrasctructure.Data;

namespace StorageProject.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllWithIncludesAsync(int skip = 0, int take = 40, CancellationToken cancellationToken = default)
        {
            return await _context.Products
                    .Include(p => p.Brand)
                    .Include(p => p.Category)
                    .AsNoTracking()
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync(cancellationToken)??Enumerable.Empty<Product>();
        }

        public async Task<Product?> GetByIdWithIncludesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id,cancellationToken);
        }
    }
}
