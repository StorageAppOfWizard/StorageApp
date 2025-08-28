using StorageProject.Domain.Entity;

namespace StorageProject.Domain.Contracts
{
    public interface IProductRepository : IRepository<Product>
    {
        public Task<IEnumerable<Product>> GetAllWithIncludesAsync(int page, int pageQuantity, CancellationToken cancellationToken = default);
        public Task<Product?> GetByIdWithIncludesAsync(Guid id, CancellationToken cancellationToken = default);
        public Task<Product?> GetByNameAsync(string name, CancellationToken cancellationToken = default);


    }
}
