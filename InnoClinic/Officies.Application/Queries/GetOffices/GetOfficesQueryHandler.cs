public class GetOfficesQueryHandler(IOfficeRepository repository) : IRequestHandler<GetOfficesQuery, ErrorOr<List<Office>>>
{
    public async Task<ErrorOr<List<Office>>> Handle(GetOfficesQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetOfficesAsync();
    }
}
