using Microsoft.AspNetCore.Mvc;
using Hizhar.Services.Contracts;
using Hizhar.Web.Infrastructure.Extensions;
using Hizhar.Web.ViewModels.Huts;

namespace Hizhar.Web.Controllers
{
    public class HutsController : Controller
    {
        private readonly IHutService hutService;
        private readonly IMountainService mountainService;

        public HutsController(IHutService hutService, IMountainService mountainService)
        {
            this.hutService = hutService;
            this.mountainService = mountainService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] AllHutsQueryModel query)
        {
            HutQueryResultViewModel result = await hutService.GetAllAsync(query);

            query.TotalHuts = result.TotalCount;
            query.Huts = result.Huts;
            query.Mountains = await mountainService.GetOptionsAsync();

            return View(query);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            HutDetailsViewModel? model = await hutService.GetDetailsAsync(id, User.GetId());

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }
    }
}
