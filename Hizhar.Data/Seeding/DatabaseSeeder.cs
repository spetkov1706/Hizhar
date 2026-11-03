using Hizhar.Common;
using Hizhar.Data.Models;
using Hizhar.Data.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Hizhar.Data.Seeding
{
    public class DatabaseSeeder
    {
        public const string AdminEmail = "admin@hizhar.bg";
        public const string AdminPassword = "Admin123!";

        public const string KeeperEmail = "keeper@hizhar.bg";
        public const string KeeperPassword = "Keeper123!";

        public const string TouristEmail = "tourist@hizhar.bg";
        public const string TouristPassword = "Tourist123!";

        private readonly HizharDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public DatabaseSeeder(
            HizharDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            await SeedRolesAsync();

            await SeedUserAsync(AdminEmail, AdminPassword, RoleConstants.Administrator);
            ApplicationUser keeper = await SeedUserAsync(KeeperEmail, KeeperPassword, RoleConstants.HutKeeper);
            ApplicationUser tourist = await SeedUserAsync(TouristEmail, TouristPassword, null);

            await AssignHutToKeeperAsync(keeper, hutId: 1);
            await SeedDemoContentAsync(tourist);
        }
        private async Task SeedRolesAsync()
        {
            string[] roles = { RoleConstants.Administrator, RoleConstants.HutKeeper };

            foreach (string role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
        private async Task<ApplicationUser> SeedUserAsync(string email, string password, string? role)
        {
            ApplicationUser? user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };

                IdentityResult result = await userManager.CreateAsync(user, password);
                if (!result.Succeeded)
                {
                    string errors = string.Join("; ", result.Errors.Select(e => e.Description));
                    throw new InvalidOperationException($"Неуспешно създаване на {email}: {errors}");
                }
            }

            if (role != null && !await userManager.IsInRoleAsync(user, role))
            {
                await userManager.AddToRoleAsync(user, role);
            }

            return user;
        }
        private async Task AssignHutToKeeperAsync(ApplicationUser keeper, int hutId)
        {
            Hut? hut = await context.Huts.FirstOrDefaultAsync(h => h.Id == hutId && !h.IsDeleted);

            if (hut != null && hut.OwnerId == null)
            {
                hut.OwnerId = keeper.Id;
                await context.SaveChangesAsync();
            }
        }

        private async Task SeedDemoContentAsync(ApplicationUser tourist)
        {
            if (await context.Reviews.AnyAsync())
            {
                return;
            }

            DateTime now = DateTime.UtcNow;

            context.Reviews.AddRange(
                new Review
                {
                    HutId = 1,
                    UserId = tourist.Id,
                    Rating = 5,
                    CreatedOn = now.AddDays(-20),
                    Content = "Невероятна гледка към езерата, топла храна и любезен персонал. Препоръчвам!"
                },
                new Review
                {
                    HutId = 4,
                    UserId = tourist.Id,
                    Rating = 4,
                    CreatedOn = now.AddDays(-12),
                    Content = "Удобна база за Вихрен. Стаите са чисти, но вечер е доста шумно."
                },
                new Review
                {
                    HutId = 8,
                    UserId = tourist.Id,
                    Rating = 5,
                    CreatedOn = now.AddDays(-3),
                    Content = "Идеална за неделна разходка до Черни връх. Супата е страхотна."
                });

            context.TrailConditions.AddRange(
                new TrailCondition
                {
                    TrailId = 2,
                    UserId = tourist.Id,
                    ConditionType = ConditionType.Snow,
                    ReportedOn = now.AddDays(-2),
                    Description = "Над Леденото езеро има около 20 см сняг, нужни са щеки."
                },
                new TrailCondition
                {
                    TrailId = 5,
                    UserId = tourist.Id,
                    ConditionType = ConditionType.Clear,
                    ReportedOn = now.AddDays(-1),
                    Description = "Пътеката е суха и добре маркирана."
                },
                new TrailCondition
                {
                    TrailId = 4,
                    UserId = tourist.Id,
                    ConditionType = ConditionType.Mud,
                    ReportedOn = now.AddDays(-5),
                    Description = "Кал в ниската част след дъждовете, нагоре е сухо."
                });

            context.ClimbedPeaks.AddRange(
                new ClimbedPeak { UserId = tourist.Id, PeakId = 9, ClimbedOn = now.AddDays(-3).Date },
                new ClimbedPeak { UserId = tourist.Id, PeakId = 1, ClimbedOn = now.AddDays(-19).Date });

            context.FavoriteHuts.Add(new FavoriteHut { UserId = tourist.Id, HutId = 4, AddedOn = now });

            await context.SaveChangesAsync();
        }
    }
}