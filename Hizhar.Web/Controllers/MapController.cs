using Hizhar.Services.Contracts;
using Hizhar.Web.ViewModels.Map;
using Microsoft.AspNetCore.Mvc;

namespace Hizhar.Web.Controllers
{
    public class MapController : Controller
    {
        private readonly IMapService mapService;

        public MapController(IMapService mapService)
        {
            this.mapService = mapService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Data()
        {
            MapDataViewModel data = await mapService.GetMapDataAsync();
            return Json(data);
        }
    }
}
