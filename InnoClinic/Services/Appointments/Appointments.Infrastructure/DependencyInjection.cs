using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration)
                .AddEmail(configuration)
                .AddHttpClients(configuration)
                .AddCaching(configuration)
                .AddTransient<IPDFDocumentGenerator, PDFDodumentGenerator>();

        return services;
    }    
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork>()
                .AddScoped<IAppointmentsRepository, AppointmentsRepository>()
                .AddScoped<IAppointmentsResultRepository, AppointmentsResultRepository>()
                .AddScoped<IServiceRepository, ServiceRepository>();

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

    public static IServiceCollection AddEmail(this IServiceCollection services, IConfiguration configuration)
    {
        var emailSettings = new EmailSettings();
        configuration.Bind(EmailSettings.SectionName, emailSettings);

        services.AddSingleton(emailSettings)
                .AddTransient<IEmailSender, EmailSender>();

        return services;
    }

    public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IFilesHttpClient, FilesHttpClient>()
            .AddHttpClient<IFilesHttpClient, FilesHttpClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["FilesAPI"]);
            });

        services.AddScoped<IProfilesHttpClient, ProfilesHttpClient>()
            .AddHttpClient<IProfilesHttpClient, ProfilesHttpClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["ProfilesAPI"]);
            });

        services.AddScoped<IAccountsHttpClient, AccountHttpClient>()
            .AddHttpClient<IAccountsHttpClient, AccountHttpClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["AccountAPI"]);
            });

        return services;
    }

    public static IServiceCollection AddCaching(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(redisOpt =>
        {
            string connection = configuration.GetConnectionString("Redis");

            redisOpt.Configuration = connection;
        });

        services.AddSingleton<ICacheService, CacheService>();

        return services;
    }
}
