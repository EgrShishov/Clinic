public class ViewServicesQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<ViewServicesQuery, ErrorOr<List<ServiceResponse>>>
{
    public async Task<ErrorOr<List<ServiceResponse>>> Handle(ViewServicesQuery request, CancellationToken cancellationToken)
    {
        var services = await unitOfWork.Services.GetAllAsync(cancellationToken);

        if (services is null)
        {
            return Error.NotFound();
        }

        var servicesInfo = new List<ServiceResponse>();

        foreach(var service in services)
        {
            var specialization = await unitOfWork.Specializations.GetSpecializationByIdAsync(service.SpecializationId);

            if (specialization is null)
            {
                return Errors.Specialization.NotFound;
            }

            servicesInfo.Add(new ServiceResponse
            {
                ServiceCategoryName = service.ServiceCategory.ToString(),
                ServiceName = service.ServiceName,
                ServicePrice = service.ServicePrice,
                Specialization = specialization.SpecializationName
            });
        }

        return servicesInfo;
    }
}
