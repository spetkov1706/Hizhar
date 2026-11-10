using Microsoft.AspNetCore.Mvc;
using Hizhar.Common;
using Hizhar.Services.Contracts;
using Hizhar.Web.Infrastructure.Extensions;
using Hizhar.Web.ViewModels.TrailConditions;
using Hizhar.Web.ViewModels.Trails;
using Microsoft.AspNetCore.Authorization;

namespace Hizhar.Web.Controllers
{
    [Autorize]
    public class TrailConditionsController : Controller
    {
        private readonly ITrailConditionService trailConditionService;
        private readonly ITrailService trailService;

        public TrailConditionsController(ITrailConditionService trailConditionService, ITrailService trailService)
        {
            this.trailConditionService = trailConditionService;
            this.trailService = trailService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind(Prefix = nameof(TrailDetailsViewModel.NewCondition))] TrailConditionFormModel model)
        {
            if (!await trailService.ExistsAsync(model.TrailId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                TrailDetailsViewModel? details = await trailService.GetDetailsAsync(model.TrailId);
                details!.NewCondition = model;
                return View("~/Views/Trails/Details.cshtml", details);
            }

            await trailConditionService.AddAsync(model, User.GetId()!);

            TempData[MessageConstants.SuccessMessageKey] = "Благодарим! Докладът ви е публикуван.";
            return RedirectToAction("Details", "Trails", new { id = model.TrailId });
        }
    }
}
