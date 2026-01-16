using Ardalis.Result;
using StorageProject.Application.Contracts;
using StorageProject.Application.DTOs.Brand;
using StorageProject.Application.Extensions;
using StorageProject.Application.Mappers;
using StorageProject.Application.Validators;
using StorageProject.Domain.Contracts;


//TO DO: explicitar o update
namespace StorageProject.Application.Services
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BrandService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<BrandDTO>>> GetAllAsync(int page, int pageQuantity)
        {
            var entity = await _unitOfWork.BrandRepository.GetPagedAsync(page, pageQuantity);

            if (entity is null)
                return Result.Success();

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

        public async Task<Result> CreateAsync(CreateBrandDTO dto)
        {
            var validation = dto.ToValidateErrors(new BrandValidator());
            if (validation.Count != 0)
                return Result.Invalid(validation);

            var existingBrand = await _unitOfWork.BrandRepository.GetByNameAsync(dto.Name);
            if(existingBrand != null)
               return Result.Conflict($"Brand with the name '{existingBrand.Name}' already exists.");

            var entity = dto.ToEntity();

            var brand = await _unitOfWork.BrandRepository.Create(entity);
            await _unitOfWork.CommitAsync();

            return Result.SuccessWithMessage("Brand Created");
        }

        public async Task<Result> UpdateAsync(UpdateBrandDTO dto)
        {

            var validation = dto.ToValidateErrors(new BrandValidator());
            if (validation.Count != 0)
                return Result.Invalid(validation);

            var existingBrand = await _unitOfWork.BrandRepository.GetByNameAsync(dto.Name);
            if (existingBrand != null)
                return Result.Conflict($"Brand with the name {existingBrand.Name} already exists.");

            var entity = await _unitOfWork.BrandRepository.GetById(dto.Id);
            if (entity is null)
                return Result.NotFound("Brand Not Found");

            dto.ToEntity(entity);
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
