public sealed class ServiceUpdatedConsumer : IConsumer<ServiceUpdatedEvent>
{
    public Task Consume(ConsumeContext<ServiceUpdatedEvent> context)
    {
        var message = context.Message;

        Console.WriteLine($"Service {message.Id} Created: {message.ServiceName}");
        return Task.CompletedTask;
    }
}
