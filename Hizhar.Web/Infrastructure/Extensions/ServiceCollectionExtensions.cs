using Hizhar.Services;
using Hizhar.Services.Contracts;

namespace Hizhar.Web.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IMountainService, MountainService>();

            return services;
        }
    }
}
