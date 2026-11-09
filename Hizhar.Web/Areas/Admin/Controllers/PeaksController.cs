using Microsoft.AspNetCore.Mvc;
using Hizhar.Services.Contracts;
using Hizhar.Web.ViewModels.Peaks;

namespace Hizhar.Web.Areas.Admin.Controllers
{
    public class PeaksController : AdminBaseController
    {
        private readonly IPeakService peakService;
        private readonly IMountainService mountainService;

        public PeaksController(IPeakService peakService, IMountainService mountainService)
        {
            this.peakService = peakService;
            this.mountainService = mountainService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<PeakListItemViewModel> model = await peakService.GetAllAsync(new AllPeaksQueryModel(), null);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new PeakFormModel
            {
                Mountains = await mountainService.GetOptionsAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PeakFormModel model)
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

            await peakService.CreateAsync(model);

            SetSuccessMessage("Върхът е добавен.");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            PeakFormModel? model = await peakService.GetForEditAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            model.Mountains = await mountainService.GetOptionsAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, PeakFormModel model)
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

            bool edited = await peakService.EditAsync(id, model);
            if (!edited)
            {
                return NotFound();
            }

            SetSuccessMessage("Върхът е обновен.");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleted = await peakService.DeleteAsync(id);

            if (deleted)
            {
                SetSuccessMessage("Върхът е изтрит.");
            }
            else
            {
                SetErrorMessage("Върхът не е намерен.");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
