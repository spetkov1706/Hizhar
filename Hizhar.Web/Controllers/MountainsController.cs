using Microsoft.AspNetCore.Mvc;
using Hizhar.Services.Contracts;
using Hizhar.Web.Infrastructure.Extensions;
using Hizhar.Web.ViewModels.Mountains;

namespace Hizhar.Web.Controllers
{
    public class MountainsController : Controller
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
        public async Task<IActionResult> Details(int id)
        {
            MountainDetailsViewModel? model = await mountainService.GetDetailsAsync(id, User.GetId());

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }
    }
}
