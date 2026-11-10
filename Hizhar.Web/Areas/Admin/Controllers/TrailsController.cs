using Microsoft.AspNetCore.Mvc;
using Hizhar.Services.Contracts;
using Hizhar.Web.ViewModels.Trails;

namespace Hizhar.Web.Areas.Admin.Controllers
{
    public class TrailsController : AdminBaseController
    {
        private readonly ITrailService trailService;
        private readonly IMountainService mountainService;

        public TrailsController(ITrailService trailService, IMountainService mountainService)
        {
            this.trailService = trailService;
            this.mountainService = mountainService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<TrailCardViewModel> model = await trailService.GetAllAsync(new AllTrailsQueryModel());
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new TrailFormModel
            {
                Mountains = await mountainService.GetOptionsAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TrailFormModel model)
        {
            if (model.MountainId > 0 && !await mountainService.ExistsAsync(model.MountainId))
            {
                ModelState.AddModelError(nameof(model.MountainId), "Изберете съществуваща планина.");
            }

            if (!ModelState.IsValid)
            {
                model.Mountains = await mountainService.GetOptionsAsync();
                return View(model);
            }

            int id = await trailService.CreateAsync(model);

            SetSuccessMessage("Маршрутът е създаден.");
            return RedirectToAction("Details", "Trails", new { area = "", id });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            TrailFormModel? model = await trailService.GetForEditAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            model.Mountains = await mountainService.GetOptionsAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TrailFormModel model)
        {
            if (model.MountainId > 0 && !await mountainService.ExistsAsync(model.MountainId))
            {
                ModelState.AddModelError(nameof(model.MountainId), "Изберете съществуваща планина.");
            }

            if (!ModelState.IsValid)
            {
                model.Mountains = await mountainService.GetOptionsAsync();
                return View(model);
            }

            bool edited = await trailService.EditAsync(id, model);
            if (!edited)
            {
                return NotFound();
            }

            SetSuccessMessage("Маршрутът е обновен.");
            return RedirectToAction("Details", "Trails", new { area = "", id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleted = await trailService.DeleteAsync(id);

            if (deleted)
            {
                SetSuccessMessage("Маршрутът е изтрит.");
            }
            else
            {
                SetErrorMessage("Маршрутът не е намерен.");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
