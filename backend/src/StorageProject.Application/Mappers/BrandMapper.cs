using StorageProject.Application.DTOs.Brand;
using StorageProject.Domain.Entity;

namespace StorageProject.Application.Mappers
{
    public static class BrandMapper
    {

        public static BrandDTO ToDTO(this Brand brand)
        {
            return new BrandDTO
            {
                Id = brand.Id,
                Name = brand.Name
            };
        }
        public static Brand ToEntity(this CreateBrandDTO dto)
        {
            return new Brand
            {
                Id = Guid.NewGuid(),
                Name = dto.Name
            };
        }

        public static void ToEntity(this UpdateBrandDTO dto, Brand brand)
        {
            brand.Id = dto.Id;
            brand.Name = dto.Name;

        }


    }
}
