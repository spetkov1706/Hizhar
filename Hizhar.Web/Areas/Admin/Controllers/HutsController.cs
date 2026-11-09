using Hizhar.Services.Contracts;
using Hizhar.Web.ViewModels.Huts;
using Microsoft.AspNetCore.Mvc;

namespace Hizhar.Web.Areas.Admin.Controllers
{
    public class HutsController : AdminBaseController
    {
        private readonly IHutService hutService;
        private readonly IMountainService mountainService;

        public HutsController(IHutService hutService, IMountainService mountainService)
        {
            this.hutService = hutService;
            this.mountainService = mountainService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<AdminHutListItemViewModel> model = await hutService.GetAllForAdminAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new HutFormModel
            {
                Mountains = await mountainService.GetOptionsAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(HutFormModel model)
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

            int id = await hutService.CreateAsync(model);

            SetSuccessMessage("Хижата е създадена.");
            return RedirectToAction("Details", "Huts", new { area = "", id });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            HutFormModel? model = await hutService.GetForEditAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            model.Mountains = await mountainService.GetOptionsAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, HutFormModel model)
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

            bool edited = await hutService.EditAsync(id, model);
            if (!edited)
            {
                return NotFound();
            }

            SetSuccessMessage("Хижата е обновена.");
            return RedirectToAction("Details", "Huts", new { area = "", id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            HutDeleteViewModel? model = await hutService.GetForDeleteAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ActionName(nameof(Delete))]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool deleted = await hutService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            SetSuccessMessage("Хижата е изтрита.");
            return RedirectToAction(nameof(Index));
        }
    }
}
