namespace StorageApp.Orders.Domain.Entity
    //mesmo namespace do micro de orders
{
    public record class OrderMessage
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int QuantityProduct { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
