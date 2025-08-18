using StorageProject.Domain.Entity;

namespace StorageProject.Domain.Contracts
{
    public interface IProductRepository : IRepository<Product>
    {
        public Task<IEnumerable<Product>> GetAllWithIncludesAsync(int skip = 0, int take = 40, CancellationToken cancellationToken = default);
        public Task<Product?> GetByIdWithIncludesAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
