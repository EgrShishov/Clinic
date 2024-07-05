using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Auth.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Auth.Infrastructure.EmaiService;
using Auth.Application.Common.Abstractions;
using Auth.Infrastructure.Persistence.Repository;

namespace Auth.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistense(configuration);
            services.AddEmailService(configuration);

            return services;
        }

        public static IServiceCollection AddEmailService(this IServiceCollection services, IConfiguration configuration)
        {
            EmailSettings emailSettings = new EmailSettings();
            configuration.Bind(EmailSettings.SectionName, emailSettings);

            services.AddSingleton<IEmailSender, EmailSender>();
            return services;
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

        public static IServiceCollection AddPersistense(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistence()
                    .AddDbContext<AuthDbContext>(
                           options =>
                               options.UseNpgsql(
                                   configuration.GetConnectionString("AuthorizationDb")));
            return services;
        }
    }
}
