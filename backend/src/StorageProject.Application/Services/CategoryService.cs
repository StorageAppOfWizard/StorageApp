using Ardalis.Result;
using StorageProject.Application.Contracts;
using StorageProject.Application.DTOs.Category;
using StorageProject.Application.Extensions;
using StorageProject.Application.Mappers;
using StorageProject.Application.Validators;
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

            if (entity is null)
                return Result<List<CategoryDTO>>.NotFound("No categories found.");

            return Result.Success(entity.Select(b => b.ToDTO()).ToList());
        }

        public async Task<Result<CategoryDTO>> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                return Result.Error("Invalid category ID provided.");

            var entity = await _unitOfWork.CategoryRepository.GetById(id);

            if (entity is null)
                return Result.NotFound("Category not found");

            return Result.Success(entity.ToDTO());
        }

        public async Task<Result> CreateAsync(CreateCategoryDTO createCategoryDTO)
        {
            var validation = createCategoryDTO.ToValidateErrors(new CategoryValidator());
            if (validation.Any())
                return Result.Invalid(validation);

            var existingCategory = await _unitOfWork.CategoryRepository.GetByNameAsync(createCategoryDTO.Name);
            if (existingCategory is not null)
                return Result.Conflict($"Category with the name {existingCategory.Name} already exists.");

            var entity = createCategoryDTO.ToEntity();
            var result = await _unitOfWork.CategoryRepository.Create(entity);

            await _unitOfWork.CommitAsync();
            return Result.SuccessWithMessage($"{createCategoryDTO.Name} created!");
        }

        public async Task<Result> UpdateAsync(UpdateCategoryDTO updateCategoryDTO)
        {

            var validation = updateCategoryDTO.ToValidateErrors(new CategoryValidator());
            if (validation.Any())
                return Result.Invalid(validation);

            var entity = await _unitOfWork.CategoryRepository.GetById(updateCategoryDTO.Id);
            if (entity is null)
                return Result.NotFound("Category not found"); 

            var existingCategory = await _unitOfWork.CategoryRepository.GetByNameAsync(updateCategoryDTO.Name);
            if (existingCategory is not null)
                return Result.Conflict($"Category with the name {existingCategory.Name} already exists.");

            updateCategoryDTO.ToEntity(entity);
            await _unitOfWork.CommitAsync();

            return Result.SuccessWithMessage($"{updateCategoryDTO.Name} changed");

        }

        public async Task<Result> RemoveAsync(Guid id)
        {
            if (id == Guid.Empty)
                return Result.Error("Invalid category ID provided.");

            var entity = await _unitOfWork.CategoryRepository.GetById(id);

            if (entity is null)
                return Result.NotFound("Category not found");

            _unitOfWork.CategoryRepository.Delete(entity);
            await _unitOfWork.CommitAsync();
            return Result.SuccessWithMessage("Category deleted successfully.");
        }

    }
}
