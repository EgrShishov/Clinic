using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        services.AddHttpClient<IAccountService, AccountService>(client =>
        {
            client.BaseAddress = new Uri(configuration.GetSection("IdentityService").Value);
        });
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
