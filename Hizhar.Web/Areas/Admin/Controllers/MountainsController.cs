using Microsoft.AspNetCore.Mvc;
using Hizhar.Services.Contracts;
using Hizhar.Services.Models;
using Hizhar.Web.ViewModels.Mountains;

namespace Hizhar.Web.Areas.Admin.Controllers
{
    public class MountainsController : AdminBaseController
    {
        private readonly IMountainService mountainService;

        public MountainsController(IMountainService mountainService)
        {
            this.mountainService = mountainService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<MountainIndexViewModel> model = await mountainService.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new MountainFormModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(MountainFormModel model)
        {
            if (await mountainService.NameExistsAsync(model.Name ?? string.Empty))
            {
                ModelState.AddModelError(nameof(model.Name), "Вече има планина с това име.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await mountainService.CreateAsync(model);

            SetSuccessMessage("Планината е добавена.");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            MountainFormModel? model = await mountainService.GetForEditAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, MountainFormModel model)
        {
            if (await mountainService.NameExistsAsync(model.Name ?? string.Empty, id))
            {
                ModelState.AddModelError(nameof(model.Name), "Вече има планина с това име.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool edited = await mountainService.EditAsync(id, model);
            if (!edited)
            {
                return NotFound();
            }

            SetSuccessMessage("Планината е обновена.");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            OperationResult result = await mountainService.DeleteAsync(id);

            if (result.Succeeded)
            {
                SetSuccessMessage("Планината е изтрита.");
            }
            else
            {
                SetErrorMessage(result.ErrorMessage);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
