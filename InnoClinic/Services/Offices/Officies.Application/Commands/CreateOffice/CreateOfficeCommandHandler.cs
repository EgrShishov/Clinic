public class CreateOfficeCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateOfficeCommand, ErrorOr<Office>>
{
    public async Task<ErrorOr<Office>> Handle(CreateOfficeCommand request, CancellationToken cancellationToken)
    {
        var office = new Office
        {
            City = request.City,
            HouseNumber = request.HouseNumber,
            IsActive = request.IsActive,
            OfficeNumber = request.OfficeNumber,
            PhotoId = request.PhotoId,
            RegistryPhoneNumber = request.RegistryPhoneNumber,
            Street = request.Street
        };

        await unitOfWork.OfficeRepository.AddOfficeAsync(office, unitOfWork.Session);
        return office;
    }
}
