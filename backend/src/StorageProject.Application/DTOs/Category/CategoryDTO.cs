namespace StorageProject.Application.DTOs.Category
{
    public record CategoryDTO
    {
        public required Guid Id { get; init; }
        public required string Name { get; init; }
        public string? Description { get; init; }
    }
}
