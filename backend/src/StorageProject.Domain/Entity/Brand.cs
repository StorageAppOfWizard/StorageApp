using StorageProject.Domain.Abstractions;

namespace StorageProject.Domain.Entity
{
    public class Brand : EntityBase
    {
        public string Name { get; set; } = string.Empty;

        public ICollection<Product> Products { get; set; }

        public Brand() { }

        public Brand(string name)
        {
            Name = name;
        }
    }
}
