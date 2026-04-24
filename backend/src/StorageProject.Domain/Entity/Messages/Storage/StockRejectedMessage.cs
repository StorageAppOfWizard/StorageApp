namespace StorageProject.Domain.Entity.Messages.Storage
{
    public class StockRejectedMessage
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public bool StockAvailable { get; set; }
    }
}
