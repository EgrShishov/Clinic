using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        return services;
    }
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        BlobStorageSettings storageSettings = new BlobStorageSettings();
        //configuration.Bind(BlobStorageSettings.SectionName, storageSettings); //TODO: switch to BlobStorageSettings

        services.AddSingleton<IBlobStorageService, BlobStorageService>();
        services.AddSingleton(_ => new BlobServiceClient(configuration.GetConnectionString("BlobStorage")));
        return services;
    }
}