using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using static Hizhar.Common.MessageConstants;

namespace Hizhar.Web.ViewModels.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AllowedFileExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] extensions;

        public AllowedFileExtensionsAttribute(string commaSeparatedExtensions)
        {
            extensions = commaSeparatedExtensions
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(e => e.ToLowerInvariant())
                .ToArray();

            ErrorMessage = string.Format(InvalidFileExtension, string.Join(", ", extensions));
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                string extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!extensions.Contains(extension))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}
