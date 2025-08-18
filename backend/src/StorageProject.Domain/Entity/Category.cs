using StorageProject.Domain.Abstractions;

namespace StorageProject.Domain.Entity
{
    public class Category : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;

        public ICollection<Product> Products { get; set; }

        public Category() { }

        public Category(string name, string? description)
        {
            Name = name;
            Description = description;
        }
        public Category(string name)
        {

            Name = name;
        }
    }
}
