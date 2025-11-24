namespace StorageProject.Domain.Abstractions
{
   public abstract class EntityBase
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; protected internal set; } = DateTime.UtcNow;
    }
}
