using Ardalis.Result;
using StorageProject.Application.Contracts;
using StorageProject.Application.DTOs.Product;
using StorageProject.Application.Mappers;
using StorageProject.Application.Validators;
using StorageProject.Domain.Contracts;

namespace StorageProject.Application.Services
{
    public class ProductService : IProductService
    {
        private IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<Result<List<ProductDTO>>> GetAllAsync()
        {
            var products = await _unitOfWork.ProductRepository.GetAllWithIncludesAsync();

            if (!products.Any())
                return Result.NotFound("Products NotFound");
            
            var dto = products.Select(product => product.ToDTO()).ToList();
            return dto;
        }

        public async Task<Result<ProductDTO>> GetByIdAsync(Guid id)
        {
            var entity = await _unitOfWork.ProductRepository.GetByIdWithIncludesAsync(id);

            if (entity is null)
                return Result.NotFound("Not Found Product");

            return Result<ProductDTO>.Success(entity.ToDTO());
        }

        public async Task <Result>CreateAsync(CreateProductDTO createProductDTO)
        {
            var validator = new ProductValidator().Validate(createProductDTO);
            if (!validator.IsValid)
                return Result.Invalid();

            var existingProduct = await _unitOfWork.ProductRepository.GetByNameAsync(createProductDTO.Name);
            if (existingProduct is not null)
                return Result.Conflict($"Product with the name {existingProduct.Name} already exists.");

            var entity = createProductDTO.ToEntity();
            await _unitOfWork.ProductRepository.Create(entity);
            await _unitOfWork.CommitAsync();

            return Result.SuccessWithMessage($"{createProductDTO.Name} Created");
        }

        public async Task<Result> UpdateAsync(UpdateProductDTO updateProductDTO)
        {
            var validator = new ProductValidator().Validate(updateProductDTO);
            if (!validator.IsValid)
                return Result.Invalid();

            var existingProduct = await _unitOfWork.ProductRepository.GetByNameAsync(updateProductDTO.Name);
            if (existingProduct is not null)
                return Result.Conflict($"Product with the name {existingProduct.Name} already exists.");

            var entity = await _unitOfWork.ProductRepository.GetById(updateProductDTO.Id);
            if (entity is null)
                return Result.NotFound("Not Found Product");

            updateProductDTO.ToEntity(entity);
            await _unitOfWork.CommitAsync();//The EF detect the tracking, don't need .Update() function

            return Result.SuccessWithMessage($"{updateProductDTO.Name} changed");
        }

        public async Task<Result> UpdateQuantityAsync(UpdateProductQuantityDTO quantityDTO)
        {
            var entity = await _unitOfWork.ProductRepository.GetById(quantityDTO.Id);
            quantityDTO.ToEntity(entity);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public async Task<Result> RemoveAsync(Guid id)
        {
            var product = await _unitOfWork.ProductRepository.GetById(id);
            if (product is null)
                return Result.NotFound("Not Found Product");

            _unitOfWork.ProductRepository.Delete(product);

            await _unitOfWork.CommitAsync();

            return Result.SuccessWithMessage("Product was deleted with sucess");
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }


    }
}
