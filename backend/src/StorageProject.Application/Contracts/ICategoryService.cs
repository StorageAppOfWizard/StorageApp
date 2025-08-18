using Ardalis.Result;
using StorageProject.Application.DTOs.Category;

namespace StorageProject.Application.Contracts
{
    public interface ICategoryService
    {

        public Task<Result<List<CategoryDTO>>> GetAllAsync();
        public Task<Result<CategoryDTO>> GetByIdAsync(Guid id);
        public Task<Result<CategoryDTO>> CreateAsync(CreateCategoryDTO createCategoryDTO);
        public Task<Result> UpdateAsync(UpdateCategoryDTO updateCategoryDTO);
        public Task<Result> RemoveAsync(Guid id);
    }
}
