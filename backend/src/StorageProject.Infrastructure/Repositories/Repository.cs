using Microsoft.EntityFrameworkCore;
using StorageProject.Domain.Abstractions;
using StorageProject.Domain.Contracts;
using StorageProject.Infrasctructure.Data;
using System.Linq;
using System.Linq.Expressions;

namespace StorageProject.Infrastructure.Repositories
{
    public abstract class Repository<T>(AppDbContext context) : IRepository<T> where T : EntityBase
    {

        private readonly DbSet<T> _dbSet = context.Set<T>();

        public async Task<T> Create(T entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            return entity;
        }

        public void Delete(T entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Remove(entity);
        }


        public async Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken = default)
            => await _dbSet
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        public async Task<T?> GetById(Guid id, CancellationToken cancellationToken = default)
            => await _dbSet
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        public async Task<IEnumerable<T?>> GetPagedAsync(int page, int pageQuantity, CancellationToken cancellationToken = default)
        {

            return await _dbSet
                .Skip((page - 1) * pageQuantity)
                .Take(pageQuantity)
                .ToListAsync(cancellationToken);
        }


        public async Task<T?> GetByConditionAsync(Expression<Func<T, bool>> predicate)

            => await _dbSet.FirstOrDefaultAsync(predicate);

        public void Update(T entity, CancellationToken cancellationToken = default)
        => _dbSet.Update(entity);

    }
}
