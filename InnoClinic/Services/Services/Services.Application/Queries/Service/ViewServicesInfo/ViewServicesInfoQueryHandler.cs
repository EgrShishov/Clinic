public class ViewServicesInfoQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<ViewServicesInfoQuery, ErrorOr<ServiceInfoResponse>>
{
    public async Task<ErrorOr<ServiceInfoResponse>> Handle(ViewServicesInfoQuery request, CancellationToken cancellationToken)
    {
        var service = await unitOfWork.Services.GetServiceByIdAsync(request.Id);
        if (service is null)
        {
            return Error.NotFound();
        }

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
