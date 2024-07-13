public class CreateServiceCommandHandler(IUnitOfWork unitOfWork, IEventBus eventBus) 
    : IRequestHandler<CreateServiceCommand, ErrorOr<ServiceInfoResponse>>
{
    public async Task<ErrorOr<ServiceInfoResponse>> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
    {
        var service = new Service
        {
            ServiceName = request.ServiceName,
            ServicePrice = request.ServicePrice,
            ServiceCategoryId = request.ServiceCategoryId,
            IsActive = request.IsActive,
            SpecializationId = request.SpecializationId
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

        var category = await unitOfWork.Categories.GetServiceCategoryByIdAsync(newService.ServiceCategoryId);
        if (category is null)
        {
            return Errors.Category.NotFound;
        }

        return new ServiceInfoResponse(
            newService.Id,
            category.CategoryName,
            newService.ServiceName,
            newService.ServicePrice,
            newService.IsActive);
    }
}
