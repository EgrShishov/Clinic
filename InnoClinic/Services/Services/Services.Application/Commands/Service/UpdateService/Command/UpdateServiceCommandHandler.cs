public class UpdateServiceCommandHandler(IUnitOfWork unitOfWork, IEventBus eventBus) 
    : IRequestHandler<UpdateServiceCommand, ErrorOr<ServiceInfoResponse>>
{
    public async Task<ErrorOr<ServiceInfoResponse>> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
    {
        var service = await unitOfWork.Services.GetServiceByIdAsync(request.Id);
        if (service is null)
        {
            return Error.NotFound("");
        }

        service.ServicePrice = request.ServicePrice;
        service.ServiceName = request.ServiceName;
        service.ServiceCategory = request.ServiceCategory;
        service.IsActive = request.IsActive;

        await unitOfWork.Services.UpdateServiceAsync(service, cancellationToken);
        await unitOfWork.SaveChangesAsync();

        await eventBus.PublishAsync(new ServiceUpdatedEvent
        {
            Id = service.Id,
            ServiceName = service.ServiceName,
            ServiceCategory = service.ServiceCategory,
            IsActive = service.IsActive,
        });

        return new ServiceInfoResponse(
            service.Id,
            service.ServiceCategory.ToString(),
            service.ServiceName,
            service.ServicePrice,
            service.IsActive);
    }
}
