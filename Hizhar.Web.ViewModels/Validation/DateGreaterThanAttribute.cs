using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using static Hizhar.Common.MessageConstants;

namespace Hizhar.Web.ViewModels.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DateGreaterThanAttribute : ValidationAttribute, IClientModelValidator
    {
        public DateGreaterThanAttribute(string otherProperty)
        {
            OtherProperty = otherProperty;
            ErrorMessage = CheckOutBeforeCheckIn;
        }

        public string OtherProperty { get; }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var otherPropertyInfo = validationContext.ObjectType.GetProperty(OtherProperty);
            if (otherPropertyInfo == null)
            {
                return new ValidationResult($"Непознато свойство: {OtherProperty}");
            }

            object? otherValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance);

            if (value is DateTime current && otherValue is DateTime other && current.Date <= other.Date)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            return ValidationResult.Success;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-dategreaterthan", FormatErrorMessage(context.ModelMetadata.GetDisplayName()));
            MergeAttribute(context.Attributes, "data-val-dategreaterthan-other", OtherProperty);
        }

        private static void MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (!attributes.ContainsKey(key))
            {
                attributes.Add(key, value);
            }
        }
    }
}
