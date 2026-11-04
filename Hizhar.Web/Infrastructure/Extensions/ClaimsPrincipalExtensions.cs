using System.Security.Claims;
using Hizhar.Common;

namespace Hizhar.Web.Infrastructure.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string? GetId(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return user.IsInRole(RoleConstants.Administrator);
        }

        public static bool IsHutKeeper(this ClaimsPrincipal user)
        {
            return user.IsInRole(RoleConstants.HutKeeper);
        }
    }
}
