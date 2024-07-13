public class ChangeServiceStatusCommandHandler(IUnitOfWork unitOfWork, IEventBus eventBus) 
    : IRequestHandler<ChangeServiceStatusCommand, ErrorOr<ServiceInfoResponse>>
{
    public async Task<ErrorOr<ServiceInfoResponse>> Handle(ChangeServiceStatusCommand request, CancellationToken cancellationToken)
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

        var category = await unitOfWork.Categories.GetServiceCategoryByIdAsync(service.ServiceCategoryId);
        if (category is null)
        {
            return Errors.Category.NotFound;
        }

        return new ServiceInfoResponse(
            service.Id,
            category.CategoryName,
            service.ServiceName,
            service.ServicePrice,
            service.IsActive);
    }
}
