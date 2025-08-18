namespace StorageProject.Application.DTOs.Category
{
    public record UpdateCategoryDTO : CreateCategoryDTO
    {
        public Guid Id { get; init; }
    }
}
