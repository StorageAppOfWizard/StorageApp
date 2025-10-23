namespace StorageProject.Application.DTOs.Order
{
    public record CreateOrderDTO
    {
        public Guid ProductId { get; init; }
        public int Quantity { get; set; }
        public string UserId{ get; set; }
    }
}
