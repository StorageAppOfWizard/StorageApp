namespace StorageProject.Application.DTOs.Messages
{
    public record class OrderCreatedMessage
    {
        public Guid Id { get; init; }
        public Guid ProductId { get; init; }
        public int QuantityProduct { get; init; }
        public string UserId { get; init; }
        public string UserName { get; init; }
        public DateTime CreatedAt { get; init; }

    }
}
