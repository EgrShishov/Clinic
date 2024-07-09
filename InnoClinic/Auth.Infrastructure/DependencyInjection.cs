using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistense(configuration);
        services.AddEmailService(configuration);
        services.AddRoles();

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

    public static IServiceCollection AddRoles(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("Doctor", policy => policy.RequireRole("Doctor"));
            options.AddPolicy("Patient", policy => policy.RequireRole("Patient"));
            options.AddPolicy("Receptionist", policy => policy.RequireRole("Receptionist"));
        });

        return services;
    }
}
