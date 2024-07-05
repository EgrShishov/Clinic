using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Profiles.Infrastructure.Persistence.Data;
using Profiles.Infrastructure.Persistence.Repository;

namespace Profiles.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistence(configuration);
            return services;
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistence()
                .AddDbContext<ProfilesDbContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString("ProfilesDb")));
            return services;
        }
    }
}
