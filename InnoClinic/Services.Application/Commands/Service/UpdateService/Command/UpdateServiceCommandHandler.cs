﻿public class UpdateServiceCommandHandler(IUnitOfWork unitOfWork, IEventBus eventBus) : IRequestHandler<UpdateServiceCommand, ErrorOr<Service>>
{
    public async Task<ErrorOr<Service>> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
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
        service.SpecializationId = request.SpecializationId;

        await unitOfWork.Services.UpdateServiceAsync(service, cancellationToken);
        await unitOfWork.SaveChangesAsync();

        await eventBus.PublishAsync(new ServiceUpdatedEvent
        {
            Id = service.Id,
            ServiceName = service.ServiceName
        });
        return service;
    }
}