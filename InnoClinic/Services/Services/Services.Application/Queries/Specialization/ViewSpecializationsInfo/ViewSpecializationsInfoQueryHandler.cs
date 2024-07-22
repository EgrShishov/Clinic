public class ViewSpecializationsInfoQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<ViewSpecializationsInfoQuery, ErrorOr<SpecializationInfoResponse>>
{
    public async Task<ErrorOr<SpecializationInfoResponse>> Handle(ViewSpecializationsInfoQuery request, CancellationToken cancellationToken)
    {
        var specialization = await unitOfWork.Specializations.GetSpecializationByIdAsync(request.Id, cancellationToken);
        if (specialization is null)
        {
            return Error.NotFound();
        }

        var allServices = await unitOfWork.Services.GetAllAsync(cancellationToken);
        var relatedServices = new List<ServiceInfoResponse>();

        foreach(var service in allServices)
        {
            if (service.SpecializationId != specialization.Id)
            {
                continue;
            }

            var category = await unitOfWork.Categories.GetServiceCategoryByIdAsync(service.ServiceCategoryId);
            if (category is null)
            {
                return Errors.Category.NotFound;
            }

            relatedServices.Add(new ServiceInfoResponse
            {
                Id = service.Id,
                IsActive = service.IsActive,
                ServiceCategory = category.CategoryName,
                ServiceName = service.ServiceName,
                ServicePrice = service.ServicePrice
            });

        }

        return new SpecializationInfoResponse(
            specialization.SpecializationName,
            specialization.IsActive ? "Active" : "Inactive",
            relatedServices);
    }
}
