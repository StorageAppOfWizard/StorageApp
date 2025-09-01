using StorageProject.Domain.Entity;

namespace StorageProject.Domain.Contracts
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}
