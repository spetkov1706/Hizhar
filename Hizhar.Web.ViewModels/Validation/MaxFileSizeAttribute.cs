using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using static Hizhar.Common.MessageConstants;

namespace Hizhar.Web.ViewModels.Validation
{
    public class MaxFileSizeAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly int maxBytes;

        public MaxFileSizeAttribute(int maxBytes)
        {
            this.maxBytes = maxBytes;
            ErrorMessage = string.Format(FileTooLarge, maxBytes / 1024 / 1024);
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IFormFile file && file.Length > maxBytes)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.TryAdd("data-val", "true");
            context.Attributes.TryAdd("data-val-maxfilesize", ErrorMessage!);
            context.Attributes.TryAdd("data-val-maxfilesize-max", maxBytes.ToString());
        }
    }
}
