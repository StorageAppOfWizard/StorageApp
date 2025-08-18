using StorageProject.Domain.Abstractions;

namespace StorageProject.Domain.Contracts
{
    public interface IRepository<T> where T : EntityBase
    {
        public Task<T> Create(T entity, CancellationToken cancellationToken = default);
        public void Update(T entity, CancellationToken cancellationToken = default);
        public void Delete(T entity, CancellationToken cancellationToken = default);
        public Task<T> GetById(Guid id, CancellationToken cancellationToken = default);
        public Task<IEnumerable<T>> GetAll(int skip = 0, int take=40, CancellationToken cancellationToken = default);

    }
}
