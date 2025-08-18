using StorageProject.Application.DTOs.Category;
using StorageProject.Domain.Entity;

namespace StorageProject.Application.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryDTO ToDTO(this Category category)
        {
            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description

            };
        }
        public static Category ToEntity(this CreateCategoryDTO dto)
        {
            return new Category
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description

            };
        }

        public static void ToEntity(this UpdateCategoryDTO dto, Category category)
        {
            category.Id = dto.Id;
            category.Name = dto.Name;
            category.Description = dto.Description;

        }
    }
}
