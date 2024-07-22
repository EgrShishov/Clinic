public sealed class ServiceCreatedConsumer : IConsumer<ServiceCreatedEvent>
{
    public Task Consume(ConsumeContext<ServiceCreatedEvent> context)
    {
        var message = context.Message;

        Console.WriteLine($"Service {message.Id} Created: {message.ServiceName}, CategoryId: {message.ServiceCategoryId}");
        return Task.CompletedTask;
    }
}
