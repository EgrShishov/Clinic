public class ChangeServiceStatusCommandHandler(IUnitOfWork unitOfWork, IEventBus eventBus) : IRequestHandler<ChangeServiceStatusCommand, ErrorOr<Service>>
{
    public async Task<ErrorOr<Service>> Handle(ChangeServiceStatusCommand request, CancellationToken cancellationToken)
    {
        var service = await unitOfWork.Services.GetServiceByIdAsync(request.Id);
        if (service == null)
        {
            return Errors.Service.NotFound;
        }

        service.IsActive = request.Status;

        await unitOfWork.Services.UpdateServiceAsync(service);
        await unitOfWork.SaveChangesAsync();

        await eventBus.PublishAsync(new ServiceStatusChangedEvent
        {
            Id = service.Id,
            ServiceName = service.ServiceName,
            IsActive = service.IsActive
        });
        return service;
    }
}
