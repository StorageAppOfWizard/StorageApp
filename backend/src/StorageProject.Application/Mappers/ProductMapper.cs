using StorageProject.Application.DTOs.Product;
using StorageProject.Domain.Entity;
using System.Data;

namespace StorageProject.Application.Mappers
{
    public static class ProductMapper
    {

        public static ProductDTO ToDTO(this Product product)
        {
            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Quantity = product.Quantity,
                Status = product.Status,
                Description = product.Description ?? string.Empty,
                BrandName = product.Brand.Name ?? string.Empty,
                CategoryName = product.Category.Name ?? string.Empty,
                BrandId = product.BrandId,
                CategoryId = product.CategoryId,

                
            };
        }
        public static Product ToEntity(this CreateProductDTO dto)
        {
            return new Product
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                Quantity = dto.Quantity,
                BrandId = dto.BrandId,
                CategoryId = dto.CategoryId,
                
            };
        }

        public static void ToEntity(this UpdateProductDTO dto, Product product)
        {

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Quantity = dto.Quantity;
            product.BrandId = dto.BrandId;
            product.CategoryId = dto.CategoryId;


        }

        public static void ToEntity(this UpdateProductQuantityDTO dto, Product product)
        {
            product.Quantity = dto.Quantity;
        }


    }
}
