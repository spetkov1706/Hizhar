using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Hizhar.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult About()
        {
            return View();
        }

        public IActionResult HttpError(int statusCode)
        {
            if (statusCode == 404)
            {
                return View("NotFound");
            }

            ViewData["StatusCode"] = statusCode;
            return View("Error");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
