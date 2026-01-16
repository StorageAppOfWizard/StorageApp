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

        Task<Result<List<ProductDTO>>> GetAllAsync(int page, int pageQuantity);
        Task<Result<List<ProductDTO>>> GetAllActiveAsync(int page, int pageQuantity);
        Task<Result<ProductDTO>> GetByIdAsync(Guid id);
        Task<Result> CreateAsync(CreateProductDTO createProductDTO);
        Task<Result> UpdateAsync(UpdateProductDTO changeProductDTO);
        Task <Result> UpdateQuantityAsync(UpdateProductQuantityDTO quantityDTO);
        Task<Result> RemoveAsync(Guid id);
        Task <Result> SoftDeleteAsync(Guid id);  
    }
}
