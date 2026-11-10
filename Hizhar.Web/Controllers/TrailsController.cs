using Microsoft.AspNetCore.Mvc;
using Hizhar.Services.Contracts;
using Hizhar.Web.ViewModels.Trails;

namespace Hizhar.Web.Controllers
{
    public class TrailsController : Controller
    {
        private readonly ITrailService trailService;
        private readonly IMountainService mountainService;

        public TrailsController(ITrailService trailService, IMountainService mountainService)
        {
            this.trailService = trailService;
            this.mountainService = mountainService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] AllTrailsQueryModel query)
        {
            query.Trails = await trailService.GetAllAsync(query);
            query.Mountains = await mountainService.GetOptionsAsync();

            return View(query);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            TrailDetailsViewModel? model = await trailService.GetDetailsAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }
    }
}
