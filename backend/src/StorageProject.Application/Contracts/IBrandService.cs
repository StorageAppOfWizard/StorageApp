using Ardalis.Result;
using StorageProject.Application.DTOs.Brand;

namespace StorageProject.Application.Contracts
{
    public interface IBrandService
    {

        public Task<Result<List<BrandDTO>>> GetAllAsync();
        public Task<Result<BrandDTO>> GetByIdAsync(Guid id);
        public Task<Result<BrandDTO>> CreateAsync(CreateBrandDTO createBrandDTO);
        public Task<Result> UpdateAsync(UpdateBrandDTO changeBrandDTO);
        public Task<Result> RemoveAsync(Guid id);

    }
}
