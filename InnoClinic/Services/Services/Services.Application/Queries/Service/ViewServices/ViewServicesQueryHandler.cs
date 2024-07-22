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
            var category = await unitOfWork.Categories.GetServiceCategoryByIdAsync(service.ServiceCategoryId);
            if (category is null)
            {
                return Errors.Category.NotFound;
            }

            servicesInfo.Add(new ServiceResponse
            {
                ServiceCategoryId = service.ServiceCategoryId,
                ServiceCategoryName = category.CategoryName,
                ... // TODO:
            });
        }

        return servicesInfo;
    }
}
