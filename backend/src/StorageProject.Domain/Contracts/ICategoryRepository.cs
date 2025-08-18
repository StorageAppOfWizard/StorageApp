using StorageProject.Domain.Entity;

namespace StorageProject.Domain.Contracts
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}
