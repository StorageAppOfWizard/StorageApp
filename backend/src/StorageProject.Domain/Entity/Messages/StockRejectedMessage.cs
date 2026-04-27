namespace StorageProject.Domain.Entity.Messages
{
    public class StockRejectedMessage
    {
        public Guid Id { get; set; }
        public bool StockAvailable { get; set; }
        public Guid OrderId { get; set; }
    }
}
