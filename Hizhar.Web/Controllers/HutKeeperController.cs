using Microsoft.AspNetCore.Mvc;
using Hizhar.Common;
using Hizhar.Services.Contracts;
using Hizhar.Services.Models;
using Hizhar.Web.Infrastructure.Extensions;
using Hizhar.Web.ViewModels.HutKeeper;
using Microsoft.AspNetCore.Authorization;

namespace Hizhar.Web.Controllers
{
    [Authorize]
    public class HutKeeperController : Controller
    {
        private readonly IHutKeeperService hutKeeperService;
        private readonly IHutService hutService;
        private readonly IReservationService reservationService;

        public HutKeeperController(
            IHutKeeperService hutKeeperService,
            IHutService hutService,
            IReservationService reservationService)
        {
            this.hutKeeperService = hutKeeperService;
            this.hutService = hutService;
            this.reservationService = reservationService;
        }

        [HttpGet]
        public async Task<IActionResult> Apply()
        {
            if (User.IsHutKeeper())
            {
                return RedirectToAction(nameof(Dashboard));
            }

            if (await hutKeeperService.HasPendingRequestAsync(User.GetId()!))
            {
                TempData[MessageConstants.ErrorMessageKey] = "Вече имате заявка, която чака одобрение от администратор.";
                return RedirectToAction("Index", "Profile");
            }

            var model = new HutKeeperRequestFormModel
            {
                Huts = await hutService.GetWithoutKeeperAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Apply(HutKeeperRequestFormModel model)
        {
            if (ModelState.IsValid)
            {
                OperationResult result = await hutKeeperService.CreateRequestAsync(model, User.GetId()!);

                if (result.Succeeded)
                {
                    TempData[MessageConstants.SuccessMessageKey] = "Заявката е изпратена. Администратор ще я прегледа скоро.";
                    return RedirectToAction("Index", "Profile");
                }

                ModelState.AddModelError(string.Empty, result.ErrorMessage!);
            }

            model.Huts = await hutService.GetWithoutKeeperAsync();
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = RoleConstants.HutKeeper)]
        public async Task<IActionResult> Dashboard()
        {
            HutKeeperDashboardViewModel? model = await hutKeeperService.GetDashboardAsync(User.GetId()!);

            if (model == null)
            {
                return View("NoHut");
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = RoleConstants.HutKeeper)]
        public async Task<IActionResult> EditHut()
        {
            HutKeeperHutFormModel? model = await hutKeeperService.GetHutForEditAsync(User.GetId()!);

            if (model == null)
            {
                return View("NoHut");
            }

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.HutKeeper)]
        public async Task<IActionResult> EditHut(HutKeeperHutFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool edited = await hutKeeperService.EditHutAsync(User.GetId()!, model);
            if (!edited)
            {
                return View("NoHut");
            }

            TempData[MessageConstants.SuccessMessageKey] = "Информацията за хижата е обновена.";
            return RedirectToAction(nameof(Dashboard));
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.HutKeeper)]
        public async Task<IActionResult> Confirm(int id)
        {
            OperationResult result = await reservationService.ConfirmAsync(id, User.GetId()!);
            SetResultMessage(result, "Резервацията е потвърдена.");

            return RedirectToAction(nameof(Dashboard));
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.HutKeeper)]
        public async Task<IActionResult> Reject(int id)
        {
            OperationResult result = await reservationService.RejectAsync(id, User.GetId()!);
            SetResultMessage(result, "Резервацията е отказана.");

            return RedirectToAction(nameof(Dashboard));
        }

        private void SetResultMessage(OperationResult result, string successMessage)
        {
            if (result.Succeeded)
            {
                TempData[MessageConstants.SuccessMessageKey] = successMessage;
            }
            else
            {
                TempData[MessageConstants.ErrorMessageKey] = result.ErrorMessage;
            }
        }
    }
}
