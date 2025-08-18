using Microsoft.EntityFrameworkCore;
using StorageProject.Domain.Abstractions;
using StorageProject.Domain.Contracts;
using StorageProject.Infrasctructure.Data;

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


        public async Task<IEnumerable<T>> GetAll(int skip, int take, CancellationToken cancellationToken = default)
        =>await _dbSet
            .AsNoTracking()
            .Skip(skip)
            .Take(take)
            .ToListAsync(cancellationToken);
       

        public async Task<T> GetById(Guid id, CancellationToken cancellationToken = default)
        => await _dbSet.FirstOrDefaultAsync(e=>e.Id == id, cancellationToken);


        public void Update(T entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Update(entity);
        }


    }
}
