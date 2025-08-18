using Ardalis.Result;
using StorageProject.Application.Contracts;
using StorageProject.Application.DTOs.Product;
using StorageProject.Application.Mappers;

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


        public async Task<Result<IEnumerable<ProductDTO>>> GetAllAsync()
        {
            var products = await _unitOfWork.ProductRepository.GetAllWithIncludesAsync();

            if (!products.Any())
                return Result.NotFound("Products NotFound");
            
            var dto = products.Select(product => product.ToDTO()).ToList();

            return dto;
        }

        public async Task <Result<ProductDTO>>CreateAsync(CreateProductDTO createProductDTO)
        {
            var entity = createProductDTO.ToEntity();

            await _unitOfWork.ProductRepository.Create(entity);
            await _unitOfWork.CommitAsync();

            var created = await _unitOfWork.ProductRepository.GetByIdWithIncludesAsync(entity.Id);

            return created.ToDTO();
        }

        public async Task<Result<ProductDTO>> GetByIdAsync(Guid id)
        {
            var entity = await _unitOfWork.ProductRepository.GetByIdWithIncludesAsync(id);

            if (entity == null)
                return Result.NotFound("Not Found Product");

            return Result<ProductDTO>.Success(entity.ToDTO());
        }

        public async Task<Result<ProductDTO>> UpdateAsync(UpdateProductDTO changeProductDTO)
        {
            var entity = await _unitOfWork.ProductRepository.GetByIdWithIncludesAsync(changeProductDTO.Id);

            if (entity == null)
                return Result.NotFound("Not Found Product");

            changeProductDTO.ToEntity(entity);

            await _unitOfWork.CommitAsync();//The EF detect the tracking, don't need .Update() function

            return entity.ToDTO();
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
