using System.ComponentModel.DataAnnotations;

namespace Hizhar.Web.ViewModels.Validation
{
    public class ValidTrailPointsAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (!TrailPointsParser.TryParse(value as string, out _, out string? error))
            {
                return new ValidationResult(error);
            }

            return ValidationResult.Success;
        }
    }
}
