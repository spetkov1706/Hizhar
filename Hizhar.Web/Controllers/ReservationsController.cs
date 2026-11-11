using Microsoft.AspNetCore.Mvc;
using Hizhar.Common;
using Hizhar.Services.Contracts;
using Hizhar.Services.Models;
using Hizhar.Web.Infrastructure.Extensions;
using Hizhar.Web.ViewModels.Reservations;
using Microsoft.AspNetCore.Authorization;

namespace Hizhar.Web.Controllers
{
    [Authorize]
    public class ReservationsController : Controller
    {
        private readonly IReservationService reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            this.reservationService = reservationService;
        }

        [HttpGet]
        public async Task<IActionResult> Create(int hutId)
        {
            ReservationFormModel? model = await reservationService.GetFormAsync(hutId);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReservationFormModel model)
        {
            if (ModelState.IsValid)
            {
                OperationResult result = await reservationService.CreateAsync(model, User.GetId()!);

                if (result.Succeeded)
                {
                    TempData[MessageConstants.SuccessMessageKey] = "Резервацията е изпратена! Хижарят ще я потвърди скоро.";
                    return RedirectToAction(nameof(Mine));
                }

                ModelState.AddModelError(string.Empty, result.ErrorMessage!);
            }

            ReservationFormModel? hutInfo = await reservationService.GetFormAsync(model.HutId);
            if (hutInfo == null)
            {
                return NotFound();
            }

            model.HutName = hutInfo.HutName;
            model.BedsCount = hutInfo.BedsCount;
            model.PricePerNight = hutInfo.PricePerNight;

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            IEnumerable<ReservationListItemViewModel> model = await reservationService.GetMineAsync(User.GetId()!);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Cancel(int id)
        {
            OperationResult result = await reservationService.CancelAsync(id, User.GetId()!);

            if (result.Succeeded)
            {
                TempData[MessageConstants.SuccessMessageKey] = "Резервацията е анулирана.";
            }
            else
            {
                TempData[MessageConstants.ErrorMessageKey] = result.ErrorMessage;
            }

            return RedirectToAction(nameof(Mine));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> FreeBeds(int hutId, DateTime checkIn, DateTime checkOut)
        {
            int freeBeds = await reservationService.GetFreeBedsAsync(hutId, checkIn, checkOut);
            return Json(new { freeBeds });
        }
    }
}
