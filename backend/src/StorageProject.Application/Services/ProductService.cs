using Ardalis.Result;
using StorageProject.Application.Contracts;
using StorageProject.Application.DTOs.Product;
using StorageProject.Application.Extensions;
using StorageProject.Application.Mappers;
using StorageProject.Application.Validators;
using StorageProject.Domain.Contracts;
using StorageProject.Domain.Entities.Enums;


//TO DO: explicitar o update
namespace StorageProject.Application.Services
{
    public class ProductService : IProductService
    {
        private IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<Result<List<ProductDTO>>> GetAllAsync(int page, int pageQuantity)
        {

            var products = await _unitOfWork.ProductRepository.GetAllWithIncludesAsync(page, pageQuantity);

            if (products is null)
                return Result.Success();

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

        public async Task<Result> CreateAsync(CreateProductDTO dto)
        {
            var validation = dto.ToValidateErrors(new ProductValidator());
            if (validation.Count != 0)
                return Result.Invalid(validation);

            var existingProduct = await _unitOfWork.ProductRepository.GetByNameAsync(dto.Name);
            if (existingProduct is not null)
                return Result.Conflict($"Product with the name {existingProduct.Name} already exists.");

            var entity = dto.ToEntity();
            await _unitOfWork.ProductRepository.Create(entity);
            await _unitOfWork.CommitAsync();

            return Result.SuccessWithMessage($"{dto.Name} Created");
        }

        public async Task<Result> UpdateAsync(UpdateProductDTO dto)
        {
            var validation = dto.ToValidateErrors(new ProductValidator());
            if (validation.Count != 0)
                return Result.Invalid(validation);

            var existingProduct = await _unitOfWork.ProductRepository.GetByConditionAsync(p => p.Name == dto.Name && p.Id != dto.Id);
            if (existingProduct is not null)
                return Result.Conflict($"Product with the name {existingProduct.Name} already exists.");

            var entity = await _unitOfWork.ProductRepository.GetById(dto.Id);
            if (entity is null)
                return Result.NotFound("Not Found Product");

            dto.ToEntity(entity);
            _unitOfWork.ProductRepository.Update(entity);
            await _unitOfWork.CommitAsync();

            return Result.SuccessWithMessage($"{dto.Name} changed");
        }

        public async Task<Result> UpdateQuantityAsync(UpdateProductQuantityDTO dto)
        {
            var entity = await _unitOfWork.ProductRepository.GetById(dto.Id);
            dto.ToEntity(entity);
            _unitOfWork.ProductRepository.Update(entity);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public async Task<Result> RemoveAsync(Guid id)
        {
            var product = await _unitOfWork.ProductRepository.GetById(id);
            if (product is null)
                return Result.NotFound("Not Found Product");

            var orders = await _unitOfWork.OrderRepository.GetAll();
            var hasLinkedOrder = orders.Any(o => o.ProductId == id);
            var pendingOrders = orders.Any(o => o.Status == OrderStatus.Pending);

            if (hasLinkedOrder && pendingOrders)
                return Result.Error("Exist a order pending with this product");

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
