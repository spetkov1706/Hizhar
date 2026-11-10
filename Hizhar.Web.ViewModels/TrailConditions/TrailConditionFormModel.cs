using System.ComponentModel.DataAnnotations;
using Hizhar.Data.Models.Enums;
using static Hizhar.Common.MessageConstants;
using static Hizhar.Common.ValidationConstants.TrailCondition;

namespace Hizhar.Web.ViewModels.TrailConditions
{
    public class TrailConditionFormModel
    {
        public int TrailId { get; set; }

        [Required(ErrorMessage = RequiredField)]
        [EnumDataType(typeof(ConditionType), ErrorMessage = SelectRequired)]
        [Display(Name = "Състояние")]
        public ConditionType ConditionType { get; set; } = ConditionType.Clear;

        [Required(ErrorMessage = RequiredField)]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = LengthField)]
        [Display(Name = "Описание")]
        public string Description { get; set; } = string.Empty;
    }
}
