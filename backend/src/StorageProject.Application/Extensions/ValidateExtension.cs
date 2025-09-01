using Ardalis.Result;
using FluentValidation;

namespace StorageProject.Application.Extensions
{
    public static class ValidateExtension
    {
        public static List<ValidationError> ToValidateErrors<T, TValidator>(this T dto, TValidator validator) where TValidator : IValidator<T>
        {

           var validation = validator.Validate(dto);

            if (!validation.IsValid)
            {
                
                var errors = validation.Errors
                   .Select(e => new ValidationError
                   {
                       Identifier = e.PropertyName,
                       ErrorMessage = e.ErrorMessage,
                       ErrorCode = e.ErrorCode
                   })
                   .ToList();

                return errors;
            }
            return new();

        }
    }
}
