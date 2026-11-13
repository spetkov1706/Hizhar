using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Hizhar.Common;
using Hizhar.Services.Contracts;
using Hizhar.Services.Models;
using Hizhar.Web.Infrastructure.Extensions;
using Hizhar.Web.ViewModels.Photos;

namespace Hizhar.Web.Controllers
{
    [Authorize]
    public class PhotosController : Controller
    {
        private readonly IPhotoService photoService;

        public PhotosController(IPhotoService photoService)
        {
            this.photoService = photoService;
        }

        [HttpPost]
        [RequestSizeLimit(ValidationConstants.Photo.MaxFileSizeInBytes + 1024 * 1024)]
        public async Task<IActionResult> Upload(PhotoUploadFormModel model)
        {
            if (!ModelState.IsValid || model.Photo == null)
            {
                string errors = string.Join(" ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                TempData[MessageConstants.ErrorMessageKey] = string.IsNullOrEmpty(errors) ? "Изберете снимка." : errors;
                return RedirectToAction("Details", "Huts", new { id = model.HutId });
            }

            OperationResult result = await photoService.AddAsync(model.HutId, User.GetId()!, model.Photo, model.Caption);

            if (result.Succeeded)
            {
                TempData[MessageConstants.SuccessMessageKey] = "Снимката е качена.";
            }
            else
            {
                TempData[MessageConstants.ErrorMessageKey] = result.ErrorMessage;
            }

            return RedirectToAction("Details", "Huts", new { id = model.HutId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            int? hutId = await photoService.DeleteAsync(id, User.GetId()!, User.IsAdmin());

            if (hutId == null)
            {
                return NotFound();
            }

            TempData[MessageConstants.SuccessMessageKey] = "Снимката е изтрита.";
            return RedirectToAction("Details", "Huts", new { id = hutId });
        }
    }
}
