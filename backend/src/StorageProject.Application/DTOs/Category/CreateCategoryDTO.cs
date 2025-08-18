namespace StorageProject.Application.DTOs.Category
{
    public record CreateCategoryDTO
    {
        public required string Name { get; init; }
        public string? Description { get; init; }
    }
}
