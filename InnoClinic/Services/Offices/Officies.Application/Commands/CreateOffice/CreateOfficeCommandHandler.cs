public class CreateOfficeCommandHandler(IUnitOfWork unitOfWork, IFilesHttpClient filesHttpClient, IEventBus eventBus) 
    : IRequestHandler<CreateOfficeCommand, ErrorOr<Office>>
{
    public async Task<ErrorOr<Office>> Handle(CreateOfficeCommand request, CancellationToken cancellationToken)
    {
        var photoUrl = await filesHttpClient.UploadPhoto(request.Photo, $"{request.City}-{request.OfficeNumber}");
        if (photoUrl.IsError)
        {
            return Error.Failure();
        }

        var office = new Office
        {
            City = request.City,
            HouseNumber = request.HouseNumber,
            IsActive = request.IsActive,
            OfficeNumber = request.OfficeNumber,
            PhotoId = photoUrl.Value,
            RegistryPhoneNumber = request.RegistryPhoneNumber,
            Street = request.Street
        };

        await unitOfWork.OfficeRepository.AddOfficeAsync(office, cancellationToken);

        await eventBus.PublishAsync(new OfficeCreatedEvent
        {
            Id = office.Id,
            City = office.City,
            HouseNumber = office.HouseNumber,
            OfficeNumber = office.OfficeNumber,
            PhotoId = office.PhotoId,
            RegistryPhoneNumber = office.RegistryPhoneNumber,
            Street = office.Street
        });

        return office;
    }
}
