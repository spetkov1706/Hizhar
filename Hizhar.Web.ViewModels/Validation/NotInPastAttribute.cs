using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using static Hizhar.Common.MessageConstants;

namespace Hizhar.Web.ViewModels.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NotInPastAttribute : ValidationAttribute, IClientModelValidator
    {
        public NotInPastAttribute()
        {
            ErrorMessage = DateInPast;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime date && date.Date < DateTime.Today)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            return ValidationResult.Success;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            if (!context.Attributes.ContainsKey("data-val"))
            {
                context.Attributes.Add("data-val", "true");
            }

            if (!context.Attributes.ContainsKey("data-val-notinpast"))
            {
                context.Attributes.Add("data-val-notinpast", FormatErrorMessage(context.ModelMetadata.GetDisplayName()));
            }
        }
    }
}
