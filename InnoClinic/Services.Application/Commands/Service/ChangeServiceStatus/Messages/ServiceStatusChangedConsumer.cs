public sealed class ServiceStatusChangedConsumer : IConsumer<ServiceStatusChangedEvent>
{
    public Task Consume(ConsumeContext<ServiceStatusChangedEvent> context)
    {
        var message = context.Message;

        Console.WriteLine($"Service {message.Id} Created: {message.ServiceName}");
        return Task.CompletedTask;
    }

}
