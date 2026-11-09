using Hizhar.Data;
using Hizhar.Data.Models.Enums;
using Hizhar.Services.Contracts;
using Hizhar.Web.ViewModels.Admin;
using Microsoft.EntityFrameworkCore;

namespace Hizhar.Services
{
    public class AdminService : IAdminService
    {
        private readonly HizharDbContext context;

        public AdminService(HizharDbContext context)
        {
            this.context = context;
        }

        public async Task<AdminDashboardViewModel> GetDashboardAsync()
        {
            return new AdminDashboardViewModel
            {
                MountainsCount = await context.Mountains.CountAsync(),
                HutsCount = await context.Huts.CountAsync(h => !h.IsDeleted),
                PeaksCount = await context.Peaks.CountAsync(),
                TrailsCount = await context.Trails.CountAsync(),
                UsersCount = await context.Users.CountAsync(),
                ReservationsCount = await context.Reservations.CountAsync(),
                ReviewsCount = await context.Reviews.CountAsync(),
                PhotosCount = await context.Photos.CountAsync(),
                ConditionsCount = await context.TrailConditions.CountAsync(),
                PendingRequestsCount = await context.HutKeeperRequests.CountAsync(r => r.Status == RequestStatus.Pending)
            };
        }

        public async Task<IEnumerable<UserListItemViewModel>> GetUsersAsync()
        {
            var userRoles = await context.UserRoles
                .Join(context.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => new { ur.UserId, RoleName = r.Name })
                .ToListAsync();

            List<UserListItemViewModel> users = await context.Users
                .AsNoTracking()
                .OrderBy(u => u.Email)
                .Select(u => new UserListItemViewModel
                {
                    Id = u.Id,
                    Email = u.Email ?? string.Empty,
                    ReservationsCount = u.Reservations.Count,
                    ReviewsCount = u.Reviews.Count,
                    OwnedHutName = u.OwnedHuts.Where(h => !h.IsDeleted).Select(h => h.Name).FirstOrDefault()
                })
                .ToListAsync();

            foreach (UserListItemViewModel user in users)
            {
                user.Roles = userRoles
                    .Where(ur => ur.UserId == user.Id && ur.RoleName != null)
                    .Select(ur => ur.RoleName!)
                    .ToList();
            }

            return users;
        }
    }
}
