using Hizhar.Services;
using Hizhar.Services.Contracts;

namespace Hizhar.Web.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IMountainService, MountainService>();

            services.AddScoped<IHutService, HutService>();
            services.AddScoped<IPeakService, PeakService>();

            services.AddScoped<IAdminService, AdminService>();

            services.AddScoped<ITrailService, TrailService>();
            services.AddScoped<ITrailConditionService, TrailConditionService>();

            services.AddScoped<IReservationService, ReservationService>();

            services.AddScoped<IHutKeeperService, HutKeeperService>();

            services.AddScoped<IReviewService, ReviewService>();

            return services;
        }
    }
}
