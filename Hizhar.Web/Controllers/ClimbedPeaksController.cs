using Hizhar.Common;
using Hizhar.Services.Contracts;
using Hizhar.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hizhar.Web.Controllers
{
    [Authorize]
    public class ClimbedPeaksController : Controller
    {
        private readonly IClimbedPeakService climbedPeakService;
        private readonly IPeakService peakService;

        public ClimbedPeaksController(IClimbedPeakService climbedPeakService, IPeakService peakService)
        {
            this.climbedPeakService = climbedPeakService;
            this.peakService = peakService;
        }

        [HttpPost]
        public async Task<IActionResult> Toggle(int peakId, string? returnUrl)
        {
            if (!await peakService.ExistsAsync(peakId))
            {
                return NotFound();
            }

            bool isClimbed = await climbedPeakService.ToggleAsync(User.GetId()!, peakId);

            TempData[MessageConstants.SuccessMessageKey] = isClimbed
                ? "Поздравления! Върхът е отбелязан като изкачен."
                : "Върхът е премахнат от изкачените.";

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return LocalRedirect(returnUrl);
            }

            return RedirectToAction("Details", "Peaks", new { id = peakId });
        }
    }
}
