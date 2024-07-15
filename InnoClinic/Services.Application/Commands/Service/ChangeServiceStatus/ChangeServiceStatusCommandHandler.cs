public class ChangeServiceStatusCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ChangeServiceStatusCommand, ErrorOr<Service>>
{
    public async Task<ErrorOr<Service>> Handle(ChangeServiceStatusCommand request, CancellationToken cancellationToken)
    {
        var service = await unitOfWork.Services.GetServiceByIdAsync(request.Id);
        if (service == null)
        {
            //return Errors.Service.NotFound();
        }

        service.IsActive = request.Status;

        await unitOfWork.Services.UpdateServiceAsync(service);
        await unitOfWork.SaveChangesAsync();
        return service;
    }
}
