using Ardalis.Result;
using StorageProject.Application.Contracts;
using StorageProject.Application.DTOs.Category;
using StorageProject.Application.Mappers;
using StorageProject.Domain.Contracts;

namespace StorageProject.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<CategoryDTO>>> GetAllAsync()
        {
            var entity = await _unitOfWork.CategoryRepository.GetAll();

            if (entity == null || !entity.Any())
                return Result<List<CategoryDTO>>.NotFound("No categories found.");

            return Result.Success(entity.Select(b => b.ToDTO()).ToList());
        }

        public async Task<Result<CategoryDTO>> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                return Result.Error("Invalid category ID provided.");

            var entity = await _unitOfWork.CategoryRepository.GetById(id);

            if (entity == null)
                return Result.NotFound("Category not found");

            return Result.Success(entity.ToDTO());
        }

        public async Task<Result<CategoryDTO>> CreateAsync(CreateCategoryDTO createCategoryDTO)
        {
            var entity = createCategoryDTO.ToEntity();

            var existingCategory = await _unitOfWork.CategoryRepository.GetByNameAsync(entity.Name);

            if (existingCategory != null)
                return Result.Conflict($"Category with the name {existingCategory.Name} already exists.");


            var result = await _unitOfWork.CategoryRepository.Create(entity);

            await _unitOfWork.CommitAsync();

            return Result.Success(result.ToDTO(), "Category Created");
        }

        public async Task<Result> UpdateAsync(UpdateCategoryDTO updateCategoryDTO)
        {
            var entity = await _unitOfWork.CategoryRepository.GetById(updateCategoryDTO.Id);
            if (entity is null)
                return Result.NotFound("Category not found");

            updateCategoryDTO.ToEntity(entity);

            var existingCategory = await _unitOfWork.CategoryRepository.GetByNameAsync(entity.Name);

            if (existingCategory != null)
                return Result.Conflict($"Category with the name {existingCategory.Name} already exists.");

            await _unitOfWork.CommitAsync();

            return Result.SuccessWithMessage("Category updated successfully.");

        }

        public async Task<Result> RemoveAsync(Guid id)
        {
            if (id == Guid.Empty)
                return Result.Error("Invalid category ID provided.");

            var entity = await _unitOfWork.CategoryRepository.GetById(id);

            if (entity == null)
                return Result.NotFound("Category not found");

            _unitOfWork.CategoryRepository.Delete(entity);
            await _unitOfWork.CommitAsync();
            return Result.SuccessWithMessage("Category deleted successfully.");
        }

    }
}
