using System.ComponentModel.DataAnnotations;

namespace StorageProject.Application.DTOs.Product
{
    public record UpdateProductDTO : CreateProductDTO
    {
        [Required(ErrorMessage = "The field {0} must be filled")]
        public Guid Id { get; init; }
    }
}
