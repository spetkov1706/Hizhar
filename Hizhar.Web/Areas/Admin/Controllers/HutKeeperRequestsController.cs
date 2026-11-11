using Microsoft.AspNetCore.Mvc;
using Hizhar.Services.Contracts;
using Hizhar.Services.Models;
using Hizhar.Web.ViewModels.HutKeeper;

namespace Hizhar.Web.Areas.Admin.Controllers
{
    public class HutKeeperRequestsController : AdminBaseController
    {
        private readonly IHutKeeperService hutKeeperService;

        public HutKeeperRequestsController(IHutKeeperService hutKeeperService)
        {
            this.hutKeeperService = hutKeeperService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<HutKeeperRequestViewModel> model = await hutKeeperService.GetRequestsAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            OperationResult result = await hutKeeperService.ApproveRequestAsync(id);

            if (result.Succeeded)
            {
                SetSuccessMessage("Заявката е одобрена. Потребителят трябва да излезе и да влезе отново, за да получи ролята „Хижар“.");
            }
            else
            {
                SetErrorMessage(result.ErrorMessage);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Reject(int id)
        {
            OperationResult result = await hutKeeperService.RejectRequestAsync(id);

            if (result.Succeeded)
            {
                SetSuccessMessage("Заявката е отхвърлена.");
            }
            else
            {
                SetErrorMessage(result.ErrorMessage);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
