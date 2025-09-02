using Ardalis.Result;
using StorageProject.Application.Contracts;
using StorageProject.Application.DTOs.Brand;
using StorageProject.Application.Extensions;
using StorageProject.Application.Mappers;
using StorageProject.Application.Validators;
using StorageProject.Domain.Contracts;

namespace StorageProject.Application.Services
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BrandService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<BrandDTO>>> GetAllAsync()
        {
            var entity = await _unitOfWork.BrandRepository.GetAll();

            if (entity == null || !entity.Any())
                return Result.NotFound("No brands found");
            
            return Result.Success(entity.Select(b => b.ToDTO()).ToList());
        }

        public async Task<Result<BrandDTO>> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                return Result.Error("Invalid brand ID provided.");

            var entity = await _unitOfWork.BrandRepository.GetById(id);

            if (entity is null)
                return Result.NotFound("Brand not found");
            
            return Result.Success(entity.ToDTO());
        }

        public async Task<Result> CreateAsync(CreateBrandDTO createBrandDTO)
        {
            var validation = createBrandDTO.ToValidateErrors(new BrandValidator());
            if (validation.Any())
                return Result.Invalid(validation);

            var existingBrand = await _unitOfWork.BrandRepository.GetByNameAsync(createBrandDTO.Name);
            if(existingBrand != null)
               return Result.Conflict($"Brand with the name '{existingBrand.Name}' already exists.");

            var entity = createBrandDTO.ToEntity();

            var brand = await _unitOfWork.BrandRepository.Create(entity);
            await _unitOfWork.CommitAsync();

            return Result.SuccessWithMessage("Brand Created");
        }

        public async Task<Result> UpdateAsync(UpdateBrandDTO updateBrandDTO)
        {

            var validation = updateBrandDTO.ToValidateErrors(new BrandValidator());
            if (validation.Any())
                return Result.Invalid(validation);

            var existingBrand = await _unitOfWork.BrandRepository.GetByNameAsync(updateBrandDTO.Name);
            if (existingBrand != null)
                return Result.Conflict($"Brand with the name {existingBrand.Name} already exists.");

            var entity = await _unitOfWork.BrandRepository.GetById(updateBrandDTO.Id);
            if (entity is null)
                return Result.NotFound("Brand Not Found");

            updateBrandDTO.ToEntity(entity);
            await _unitOfWork.CommitAsync();

            return Result.SuccessWithMessage("Brand updated successfully.");
        }

        public async Task<Result> RemoveAsync(Guid id)
        {
            if (id == Guid.Empty)
                return Result.Error("Invalid brand ID provided.");

            var entity = await _unitOfWork.BrandRepository.GetById(id);

            if (entity == null)
                return Result.NotFound("Brand not found");

            _unitOfWork.BrandRepository.Delete(entity);
            await _unitOfWork.CommitAsync();
            return Result.SuccessWithMessage("Brand deleted successfully.");
        }
    }
}
