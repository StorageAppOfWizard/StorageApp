using StorageProject.Domain.Entity;

namespace StorageProject.Domain.Contracts
{
    public interface IBrandRepository : IRepository<Brand>
    {
        Task<Brand> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}
