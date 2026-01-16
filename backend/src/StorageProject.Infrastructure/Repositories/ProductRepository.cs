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

        public async Task<IEnumerable<Product>> GetAllWithIncludesAsync(int page, int pageQuantity, CancellationToken cancellationToken = default)
        {
            return await _context.Products
                    .Where(p => p.IsActive == true)
                    .Include(p => p.Brand)
                    .Include(p => p.Category)
                    .AsNoTracking()
                    .Skip((page - 1) * pageQuantity)
                    .Take(pageQuantity)
                    .ToListAsync(cancellationToken) ?? Enumerable.Empty<Product>();
        }

        public async Task<Product?> GetByIdWithIncludesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .Where(p => p.IsActive == true)
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }


        public async Task<Product?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .Where(p => p.IsActive == true)
                .FirstOrDefaultAsync(b => b.Name.ToLower() == name.ToLower(), cancellationToken);
        }

        public async Task SoftDelete(Guid id, CancellationToken cancellationToken = default)
        {
            var product = _context.Products.FirstAsync(p => p.Id == id, cancellationToken);

            product.Result.Deactivate();


        }

    }
}
