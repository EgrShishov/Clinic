public class CreateServiceCommandHandler(IUnitOfWork unitOfWork, IEventBus eventBus) : IRequestHandler<CreateServiceCommand, ErrorOr<Service>>
{
    public async Task<ErrorOr<Service>> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
    {
        var service = new Service
        {
            ServiceName = request.ServiceName,
            ServicePrice = request.ServicePrice,
            ServiceCategoryId = request.ServiceCategoryId,
            IsActive = request.IsActive,
            SpecializationId = request.SpecializationId,
        };

        var newService = await unitOfWork.Services.AddServiceAsync(service, cancellationToken);
        await unitOfWork.SaveChangesAsync();

        await eventBus.PublishAsync(new ServiceCreatedEvent
            {
                Id = newService.Id,
                ServiceCategoryId = newService.ServiceCategoryId,
                ServiceName = newService.ServiceName
            }, 
            cancellationToken);

        return newService;
    }
}
