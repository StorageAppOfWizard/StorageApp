using FluentValidation;
using StorageProject.Application.DTOs.Category;

namespace StorageProject.Application.Validators
{
    public class CategoryValidator : AbstractValidator<CreateCategoryDTO>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Field Name is required")
                .WithErrorCode("400")
                .Length(3, 100)
                .Matches(@"[a-zA-Z]")
                .WithMessage("Field must contain at least one letter"); ;
        }
    
    
    }
}
