using System.ComponentModel.DataAnnotations;
using Hizhar.Web.ViewModels.Huts;
using static Hizhar.Common.MessageConstants;
using static Hizhar.Common.ValidationConstants.HutKeeperRequest;

namespace Hizhar.Web.ViewModels.HutKeeper
{
    public class HutKeeperRequestFormModel
    {
        [Range(1, int.MaxValue, ErrorMessage = SelectRequired)]
        [Display(Name = "Хижа")]
        public int HutId { get; set; }

        [Required(ErrorMessage = RequiredField)]
        [StringLength(MessageMaxLength, MinimumLength = MessageMinLength, ErrorMessage = LengthField)]
        [Display(Name = "Съобщение до администратора")]
        public string Message { get; set; } = string.Empty;

        public IEnumerable<HutOptionViewModel> Huts { get; set; } = new List<HutOptionViewModel>();
    }
}
