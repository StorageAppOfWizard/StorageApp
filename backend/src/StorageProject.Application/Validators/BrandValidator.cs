using FluentValidation;
using StorageProject.Application.DTOs.Brand;

namespace StorageProject.Application.Validators
{
    public class BrandValidator : AbstractValidator<CreateBrandDTO>
    {
        public BrandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Field Name is required")
                .WithErrorCode("400")
                .Length(3, 100)
                .WithMessage("Field must contain between 3 and 100 caracteres")
                .Matches(@"[a-zA-Z]")
                .WithMessage("Field must contain at least one letter");
        }
    }
}
