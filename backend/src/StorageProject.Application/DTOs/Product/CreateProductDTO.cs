namespace StorageProject.Application.DTOs.Product
{
    public record CreateProductDTO
    {
        public string Name { get; init; }
        public string? Description { get; init; }
        public int Quantity { get; init; }
        public Guid BrandId { get; init; }
        public Guid CategoryId { get; init; }
    }
}
