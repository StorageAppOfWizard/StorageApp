using Ardalis.Result;
using StorageProject.Application.DTOs.Product;

namespace StorageProject.Application.Contracts
{
    public interface IProductService : IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        Task<Result<IEnumerable<ProductDTO>>> GetAllAsync();
        Task<Result<ProductDTO>> GetByIdAsync(Guid id);
        Task<Result<ProductDTO>> CreateAsync(CreateProductDTO createProductDTO);
        Task<Result<ProductDTO>> UpdateAsync(UpdateProductDTO changeProductDTO);
        Task<Result> RemoveAsync(Guid id);
    }
}
