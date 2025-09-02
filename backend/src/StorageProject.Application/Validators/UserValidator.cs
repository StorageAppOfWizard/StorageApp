using FluentValidation;
using StorageProject.Application.DTOs.User;

namespace StorageProject.Application.Validators
{
    public class UserValidator : AbstractValidator<CreateUserDTO>   
    {
        public UserValidator() {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Field Name is required")
                .Length(3, 20).WithMessage("Field must contain between 3 and 20 caracteres");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Field E-mail is required")
                .EmailAddress().WithMessage("Field must be a valid e-mail")
                .Length(3, 100).WithMessage("Field must contain between 3 and 100 caracteres");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Fiel Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .MaximumLength(150).WithMessage("Password must not exceed 150 characters.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one number.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

            RuleFor(x => x.PasswordConfirmed)
                .NotEmpty().WithMessage("Fiel Confirm Password is required")
                .Equal(x => x.Password).WithMessage("Passwords do not match");



        }
    }
}
