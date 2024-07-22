using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(conf => conf.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly))
                .AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        services.AddTransient<ITimeSlotsGenerator, TimeSlotsGenerator>();
        return services;
    }
}