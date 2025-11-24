using Microsoft.EntityFrameworkCore;
using StorageProject.Domain.Abstractions;
using System.Linq.Expressions;

namespace StorageProject.Domain.Contracts
{
    public interface IRepository<T> where T : EntityBase
    {
        public Task<T> Create(T entity, CancellationToken cancellationToken = default);
        public void Update(T entity, CancellationToken cancellationToken = default);
        public void Delete(T entity, CancellationToken cancellationToken = default);
        public Task<T> GetById(Guid id, CancellationToken cancellationToken = default);
        public Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken = default);
        public Task<IEnumerable<T?>> GetPagedAsync(int? page, int? pageQuantity, CancellationToken cancellationToken = default);
        public Task<T?> GetByConditionAsync(Expression<Func<T, bool>> predicate);


    }
}
