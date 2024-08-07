using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration)
                .AddHttpClients(configuration);
        return services;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork>()
                .AddScoped<IDoctorRepository, DoctorRepository>()
                .AddScoped<IOfficeRepository, OfficeRepository>()
                .AddScoped<IPatientRepository, PatientRepository>()
                .AddScoped<IReceptionistRepository, ReceptionistRepository>();
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
    public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAccountHttpClient, AccountHttpClient>()
                .AddHttpClient<IAccountHttpClient, AccountHttpClient>(client =>
                {
                    client.BaseAddress = new Uri(configuration["IdentityAPI"]);
                });

        services.AddScoped<IFilesHttpClient, FilessHttpClient>()
                .AddHttpClient<IFilesHttpClient, FilessHttpClient>(client =>
                {
                    client.BaseAddress = new Uri(configuration["DocumentsAPI"]);
                });
        return services;
    }
}
