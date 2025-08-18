namespace StorageProject.Application.DTOs.Brand
{
    public record BrandDTO
    {
        public Guid Id { get; init; }
        public required string Name { get; init; }
    }
}
