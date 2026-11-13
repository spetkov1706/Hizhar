using Hizhar.Common;
using Hizhar.Services.Contracts;
using Hizhar.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hizhar.Web.Controllers
{
    [Authorize]
    public class FavoritesController : Controller
    {
        private readonly IFavoriteService favoriteService;
        private readonly IHutService hutService;

        public FavoritesController(IFavoriteService favoriteService, IHutService hutService)
        {
            this.favoriteService = favoriteService;
            this.hutService = hutService;
        }

        [HttpPost]
        public async Task<IActionResult> Toggle(int hutId)
        {
            if (!await hutService.ExistsAsync(hutId))
            {
                return NotFound();
            }

            bool isFavorite = await favoriteService.ToggleAsync(User.GetId()!, hutId);

            TempData[MessageConstants.SuccessMessageKey] = isFavorite
                ? "Хижата е добавена в любими."
                : "Хижата е премахната от любими.";

            return RedirectToAction("Details", "Huts", new { id = hutId });
        }
    }
}
