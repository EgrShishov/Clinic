using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

public static class DependencyInjection
{
    public static IServiceCollection AddInfratructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration)
                .AddMessageBrokers(configuration);
        return services;
    } 
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork>()
                .AddScoped<IServicesRepository, ServicesRepository>()
                .AddScoped<ISpecializationsRepository, SpecializationsRepository>();
        return services;
    } 
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence();
        //add db here later
        return services;
    }

    public static IServiceCollection AddMessageBrokers(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MessageBrokerSettings>(configuration.GetRequiredSection(MessageBrokerSettings.SectionName))
                .AddSingleton(conf => conf.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);
        services.AddTransient<IEventBus, EventBus>();
        return services;
    }
}
