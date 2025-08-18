using Microsoft.EntityFrameworkCore;
using StorageProject.Domain.Contracts;
using StorageProject.Domain.Entity;
using StorageProject.Infrasctructure.Data;

namespace StorageProject.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Category> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            if (name == "" || name == null)
            {
                throw new ArgumentNullException(nameof(name), "Brand name cannot be null or empty.");
            }
            return await _context.Categories.FirstOrDefaultAsync(b => b.Name.ToLower() == name.ToLower(), cancellationToken);
        }
    }
}
