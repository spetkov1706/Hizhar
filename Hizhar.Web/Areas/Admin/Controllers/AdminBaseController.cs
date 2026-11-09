using Microsoft.AspNetCore.Mvc;
using Hizhar.Common;
using Microsoft.AspNetCore.Authorization;

namespace Hizhar.Web.Areas.Admin.Controllers
{
    [Area(RoleConstants.AdminArea)]
    [Authorize(Roles = RoleConstants.Administrator)]
    public abstract class AdminBaseController : Controller
    {
        protected void SetSuccessMessage(string message)
        {
            TempData[MessageConstants.SuccessMessageKey] = message;
        }

        protected void SetErrorMessage(string? message)
        {
            TempData[MessageConstants.ErrorMessageKey] = message;
        }
    }
}
