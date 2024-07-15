public class ViewServicesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<ViewServicesQuery, ErrorOr<List<Service>>>
{
    public async Task<ErrorOr<List<Service>>> Handle(ViewServicesQuery request, CancellationToken cancellationToken)
    {
        var services = await unitOfWork.Services.GetAllAsync(cancellationToken);
        if (services is null)
        {
            return Error.NotFound();
        }

        return services.ToList();
    }
}
