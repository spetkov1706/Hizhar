using Microsoft.AspNetCore.Mvc;
using Hizhar.Services.Contracts;
using Hizhar.Web.ViewModels.Admin;

namespace Hizhar.Web.Areas.Admin.Controllers
{
    public class UsersController : AdminBaseController
    {
        private readonly IAdminService adminService;

        public UsersController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<UserListItemViewModel> model = await adminService.GetUsersAsync();
            return View(model);
        }
    }
}
