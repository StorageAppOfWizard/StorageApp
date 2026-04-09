using StorageProject.Domain.Contracts;
using StorageProject.Infrasctructure.Data;

namespace StorageProject.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _context;
        private IProductRepository _productRepository = null!;
        private IBrandRepository _brandRepository = null!;
        private ICategoryRepository _categoryRepository = null!;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(_context);
        public IBrandRepository BrandRepository => _brandRepository ??= new BrandRepository(_context);
        public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(_context);


        public async Task CommitAsync() => await _context.SaveChangesAsync();
        public void Dispose() => _context?.Dispose();

    }
}
