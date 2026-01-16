using StorageProject.Domain.Entities.Enums;

namespace StorageProject.Application.DTOs.Product
{
    public record ProductDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public int Quantity { get; init; }
        public ProductStatus Status { get; init; }
        public string Description { get; init; } = string.Empty;
        public Guid BrandId { get; init; }
        public Guid CategoryId { get; init; }
        public string BrandName { get; init; } = string.Empty;
        public string CategoryName { get; init; } = string.Empty;
        public bool IsActive { get; init; }
    }

}
