namespace StorageProject.Domain.Contracts
{
    public interface IUnitOfWork : IDisposable
    {

        public IProductRepository ProductRepository { get; }
        public IBrandRepository BrandRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public async Task CommitAsync() { }
    }
}
