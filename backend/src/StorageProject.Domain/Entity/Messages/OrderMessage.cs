namespace StorageProject.Application.DTOs.Messages
{
    public record class OrderMessage
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int QuantityProduct { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
