using MediatR;

public class AppointmentsNotificationBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    public AppointmentsNotificationBackgroundService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            await mediator.Send(new RememberAboutUpcomingAppointmentCommand(), stoppingToken);
            await Task.Delay(TimeSpan.FromDays(1), stoppingToken); //cron template analog
        }
    }
}
