public sealed record ViewServicesQuery(int ServiceCategoryId) : IRequest<ErrorOr<List<ServiceResponse>>>
{
}

