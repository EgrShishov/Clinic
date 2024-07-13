using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

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
            
        MongoDbSettings mongoDbSettings = new();
        configuration.Bind(MongoDbSettings.SectionName, mongoDbSettings);

        services.Configure<MongoDbSettings>(configuration.GetSection("OfficesDatabase"));
        services.AddPersistence()
                .AddSingleton<IMongoClient, MongoClient>(sp =>
                {
                    return new MongoClient(mongoDbSettings.ConnectionString);
                });

        return services;
    }
}
