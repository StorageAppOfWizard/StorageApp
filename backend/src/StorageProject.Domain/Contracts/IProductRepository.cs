using StorageProject.Domain.Entity;

namespace StorageProject.Domain.Contracts
{
    public interface IProductRepository : IRepository<Product>
    {
        public Task<IEnumerable<Product>> GetAllWithIncludesAsync(CancellationToken cancellationToken = default);
        public Task<Product?> GetByIdWithIncludesAsync(Guid id, CancellationToken cancellationToken = default);
        public Task<Product?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        public Task SoftDelete(Guid id, CancellationToken cancellationToken = default);


    }
}
