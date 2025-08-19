namespace StorageProject.Application.DTOs.Product
{
    public record UpdateProductQuantityDTO
    {
        public Guid Id { get; init; }
        public int Quantity { get; init; }
    }
}
