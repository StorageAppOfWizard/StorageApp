namespace StorageProject.Domain.Entity.Messages.Order
    //mesmo namespace do micro de orders
{
    public record class OrderCreatedMessage
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int QuantityProduct { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
