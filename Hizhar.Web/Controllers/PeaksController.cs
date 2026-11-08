using Microsoft.AspNetCore.Mvc;
using Hizhar.Services.Contracts;
using Hizhar.Web.Infrastructure.Extensions;
using Hizhar.Web.ViewModels.Peaks;

namespace Hizhar.Web.Controllers
{
    public class PeaksController : Controller
    {
        private readonly IPeakService peakService;
        private readonly IMountainService mountainService;

        public PeaksController(IPeakService peakService, IMountainService mountainService)
        {
            this.peakService = peakService;
            this.mountainService = mountainService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] AllPeaksQueryModel query)
        {
            query.Peaks = await peakService.GetAllAsync(query, User.GetId());
            query.Mountains = await mountainService.GetOptionsAsync();

            return View(query);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            PeakDetailsViewModel? model = await peakService.GetDetailsAsync(id, User.GetId());

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }
    }
}
