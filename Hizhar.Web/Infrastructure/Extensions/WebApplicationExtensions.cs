using Hizhar.Data;
using Hizhar.Data.Models;
using Hizhar.Data.Seeding;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Hizhar.Web.Infrastructure.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task ApplyMigrationsAndSeedAsync(this WebApplication app)
        {
            using IServiceScope scope = app.Services.CreateScope();
            IServiceProvider services = scope.ServiceProvider;

            var context = services.GetRequiredService<HizharDbContext>();
            await context.Database.MigrateAsync();

            var seeder = new DatabaseSeeder(
                context,
                services.GetRequiredService<UserManager<ApplicationUser>>(),
                services.GetRequiredService<RoleManager<IdentityRole>>());

            await seeder.SeedAsync();
        }
    }
}
