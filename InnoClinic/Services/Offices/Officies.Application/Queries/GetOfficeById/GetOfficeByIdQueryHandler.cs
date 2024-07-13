public class GetOfficeByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetOfficeByIdQuery, ErrorOr<OfficeResponse>>
{
    public async Task<ErrorOr<OfficeResponse>> Handle(GetOfficeByIdQuery request, CancellationToken cancellationToken)
    {
        var office = await unitOfWork.OfficeRepository.GetOfficeByIdAsync(request.Id, unitOfWork.Session);
        if (office is null)
        {
            return Errors.Offices.NotFound;
        }

        return new OfficeResponse(
            office.Id,
            office.Address,
            office.PhotoId,
            office.RegistryPhoneNumber,
            office.IsActive);
    }
}
