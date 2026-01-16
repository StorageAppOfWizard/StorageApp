using Ardalis.Result;
using StorageProject.Application.DTOs.Category;

namespace StorageProject.Application.Contracts
{
    public interface ICategoryService
    {

        public Task<Result<List<CategoryDTO>>> GetAllAsync(int page, int pageQuantity);
        public Task<Result<CategoryDTO>> GetByIdAsync(Guid id);
        public Task<Result> CreateAsync(CreateCategoryDTO createCategoryDTO);
        public Task<Result> UpdateAsync(UpdateCategoryDTO updateCategoryDTO);
        public Task<Result> RemoveAsync(Guid id);
    }
}
