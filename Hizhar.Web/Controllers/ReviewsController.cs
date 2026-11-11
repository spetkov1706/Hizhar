using Microsoft.AspNetCore.Mvc;
using Hizhar.Common;
using Hizhar.Services.Contracts;
using Hizhar.Services.Models;
using Hizhar.Web.Infrastructure.Extensions;
using Hizhar.Web.ViewModels.Reviews;
using Microsoft.AspNetCore.Authorization;

namespace Hizhar.Web.Controllers
{
    [Authorize]
    public class ReviewsController : Controller
    {
        private readonly IReviewService reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReviewFormModel model)
        {
            if (!ModelState.IsValid)
            {
                string errors = string.Join(" ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                TempData[MessageConstants.ErrorMessageKey] = errors;
                return RedirectToAction("Details", "Huts", new { id = model.HutId });
            }

            OperationResult result = await reviewService.AddAsync(model, User.GetId()!);

            if (result.Succeeded)
            {
                TempData[MessageConstants.SuccessMessageKey] = "Благодарим за ревюто!";
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
            int? hutId = await reviewService.DeleteAsync(id, User.GetId()!, User.IsAdmin());

            if (hutId == null)
            {
                return NotFound();
            }

            TempData[MessageConstants.SuccessMessageKey] = "Ревюто е изтрито.";
            return RedirectToAction("Details", "Huts", new { id = hutId });
        }
    }
}
