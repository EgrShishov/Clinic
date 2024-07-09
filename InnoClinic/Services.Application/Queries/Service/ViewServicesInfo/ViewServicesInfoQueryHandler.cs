public class ViewServicesInfoQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<ViewServicesInfoQuery, ErrorOr<Service>>
{
    public async Task<ErrorOr<Service>> Handle(ViewServicesInfoQuery request, CancellationToken cancellationToken)
    {
        var service = await unitOfWork.Services.GetServiceByIdAsync(request.Id);
        if (service is null)
        {
            return Error.NotFound();
        }

        return service;
    }
}
