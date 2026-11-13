using Hizhar.Services.Contracts;
using Hizhar.Web.Infrastructure.Extensions;
using Hizhar.Web.ViewModels.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hizhar.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IProfileService profileService;

        public ProfileController(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ProfileViewModel model = await profileService.GetProfileAsync(User.GetId()!);
            return View(model);
        }
    }
}
