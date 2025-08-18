using StorageProject.Domain.Abstractions;
using StorageProject.Domain.Entities.Enums;

namespace StorageProject.Domain.Entity
{
    public class Product : EntityBase
    {
        private int _quantity;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public int Quantity { get => _quantity; set { _quantity = value; Status = UpdateStatus(); } }
        public Brand Brand { get; set; }
        public Category Category { get; set; }
        public ProductStatus Status { get; set; }
        public Guid BrandId { get; set; }
        public Guid CategoryId { get; set; }

        public Product()
        {
        }

        public Product(string name, string? description, Brand brand, Category category, int quantity)
        {
            Name = name;
            Description = description;
            Brand = brand;
            Category = category;  
            Quantity = quantity;
        }


        public ProductStatus UpdateStatus()
        {
            if (Quantity <= 0)
                return ProductStatus.Unavailable;

            if (Quantity <= 5)
                return ProductStatus.LowStock;

            return ProductStatus.Available;
        }

    }
}
