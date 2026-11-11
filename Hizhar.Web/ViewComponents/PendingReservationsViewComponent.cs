using Hizhar.Services.Contracts;
using Hizhar.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Hizhar.Web.ViewComponents
{
    public class PendingReservationsViewComponent : ViewComponent
    {
        private readonly IReservationService reservationService;

        public PendingReservationsViewComponent(IReservationService reservationService)
        {
            this.reservationService = reservationService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string? userId = UserClaimsPrincipal.GetId();

            int count = userId == null
                ? 0
                : await reservationService.GetPendingCountForKeeperAsync(userId);

            return View(count);
        }
    }
}
