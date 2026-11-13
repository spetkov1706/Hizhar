using System.ComponentModel.DataAnnotations;
using Hizhar.Web.ViewModels.Validation;
using Microsoft.AspNetCore.Http;
using static Hizhar.Common.MessageConstants;
using static Hizhar.Common.ValidationConstants.Photo;

namespace Hizhar.Web.ViewModels.Photos
{
    public class PhotoUploadFormModel
    {
        public int HutId { get; set; }

        [Required(ErrorMessage = RequiredField)]
        [AllowedFileExtensions(AllowedExtensions)]
        [MaxFileSize(MaxFileSizeInBytes)]
        [Display(Name = "Снимка")]
        public IFormFile? Photo { get; set; }

        [StringLength(CaptionMaxLength, ErrorMessage = MaxLengthField)]
        [Display(Name = "Описание")]
        public string? Caption { get; set; }
    }
}
