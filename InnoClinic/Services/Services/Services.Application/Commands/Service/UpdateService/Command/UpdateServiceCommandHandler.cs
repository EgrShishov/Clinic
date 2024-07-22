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
        service.ServiceCategoryId = request.ServiceCategoryId;
        service.IsActive = request.IsActive;

        await unitOfWork.Services.UpdateServiceAsync(service, cancellationToken);
        await unitOfWork.SaveChangesAsync();

        await eventBus.PublishAsync(new ServiceUpdatedEvent
        {
            Id = service.Id,
            ServiceName = service.ServiceName
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
