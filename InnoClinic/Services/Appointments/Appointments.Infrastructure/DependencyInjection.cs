using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration)
                .AddScoped<IProfileService, ProfileService>()
                .AddTransient<IPDFDocumentGenerator, PDFDodumentGenerator>()
                .AddHttpClient<IProfileService, ProfileService>(client =>
                    {
                        client.BaseAddress = new Uri(configuration.GetSection("ProfilesAPI").Value);
                    });

        services.AddScoped<IServiceService, ServicesService>()
                .AddHttpClient<IServiceService, ServicesService>(client =>
                {
                    client.BaseAddress = new Uri(configuration.GetSection("ServicesAPI").Value);
                });
        return services;
    }    
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        return services;
    }    
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence()
            .AddDbContext<AppointmentsDbContext>(opt =>
                opt.UseNpgsql(
                    configuration.GetConnectionString("AppointmentsDb")));
        return services;
    }

}
