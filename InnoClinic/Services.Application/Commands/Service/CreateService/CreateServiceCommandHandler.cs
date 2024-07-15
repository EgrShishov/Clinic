public class CreateServiceCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateServiceCommand, ErrorOr<Service>>
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
        return newService;
    }
}
