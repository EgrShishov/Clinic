using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Profiles.Application.Commands.Doctors.CreateDoctor;

namespace Profiles.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(conf => conf.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
            services.AddValidation();

            return services;
        }

        public static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(CreateDoctorCommandValidator).Assembly);

            return services;
        }
    }
}
