using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        services.AddTransient<IPDFDocumentGenerator, PDFDodumentGenerator>();
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
