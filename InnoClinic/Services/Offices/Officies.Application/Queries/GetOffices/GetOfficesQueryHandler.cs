public class GetOfficesQueryHandler(IOfficeRepository repository) : IRequestHandler<GetOfficesQuery, ErrorOr<List<OfficeListResponse>>>
{
    public async Task<ErrorOr<List<OfficeListResponse>>> Handle(GetOfficesQuery request, CancellationToken cancellationToken)
    {
        var offices = await repository.GetOfficesAsync();
        if (offices == null)
        {
            return Errors.Offices.NotFound;
        }

        var officesList = new List<OfficeListResponse>();

        foreach(var office in offices)
        {
            officesList.Add(new OfficeListResponse
            {
                Id = office.Id,
                Address = office.Address,
                RegistryPhoneNumber = office.RegistryPhoneNumber,
                IsActive = office.IsActive
            });
        }
        return officesList;
    }
}
