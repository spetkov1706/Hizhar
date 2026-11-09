using Hizhar.Services.Contracts;
using Hizhar.Web.ViewModels.Admin;
using Microsoft.AspNetCore.Mvc;

namespace Hizhar.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        private readonly IAdminService adminService;

        public HomeController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            AdminDashboardViewModel model = await adminService.GetDashboardAsync();
            return View(model);
        }
    }
}
