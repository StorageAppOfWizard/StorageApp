namespace StorageProject.Application.DTOs.Brand
{
    public record UpdateBrandDTO : CreateBrandDTO
    {
        public Guid Id { get; init; }
    }
}
