using Microsoft.EntityFrameworkCore;
using StorageProject.Domain.Contracts;
using StorageProject.Domain.Entity;
using StorageProject.Infrasctructure.Data;

namespace StorageProject.Infrastructure.Repositories
{
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {

        private readonly AppDbContext _context;
        public BrandRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Brand> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            if (name == "" || name== null)
            {
               throw new ArgumentNullException(nameof(name), "Brand name cannot be null or empty.");
            }
            return await _context.Brands.FirstOrDefaultAsync(b => b.Name.ToLower() == name.ToLower(), cancellationToken);
        }
    }
}
