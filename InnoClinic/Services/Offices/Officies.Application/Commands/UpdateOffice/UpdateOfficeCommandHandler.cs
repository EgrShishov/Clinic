public class UpdateOfficeCommandHandler(IUnitOfWork unitOfWork, IFilesHttpClient filesHttpClient, IEventBus eventBus) 
    : IRequestHandler<UpdateOfficeCommand, ErrorOr<Unit>>
{
    public async Task<ErrorOr<Unit>> Handle(UpdateOfficeCommand request, CancellationToken cancellationToken)
    {
        var office = await unitOfWork.OfficeRepository.GetOfficeByIdAsync(request.OfficeId, cancellationToken);
        if (office == null)
        {
            return Errors.Offices.NotFound;
        }

        var photoDeletionResult = await filesHttpClient.DeletedPhoto($"{office.City}-{office.OfficeNumber}");
        if (photoDeletionResult.IsError)
        {
            return Error.Failure();
        }

        var newPhotoUri = await filesHttpClient.UploadPhoto(request.Photo, $"{request.City}-{request.OfficeNumber}");
        if (newPhotoUri.IsError)
        {
            return Error.Failure();
        }

        office.OfficeNumber = request.OfficeNumber;
        office.HouseNumber = request.HouseNumber;
        office.RegistryPhoneNumber = request.RegistryPhoneNumber;
        office.Street = request.Street;
        office.City = request.City;
        office.IsActive = request.IsActive;
        office.PhotoId = newPhotoUri.Value;

        await unitOfWork.OfficeRepository.UpdateOfficeAsync(office, cancellationToken);

        await eventBus.PublishAsync(new OfficeChangedEvent
        {
            Id = office.Id,
            City = office.City,
            HouseNumber = office.HouseNumber,
            OfficeNumber = office.OfficeNumber,
            PhotoId = office.PhotoId,
            RegistryPhoneNumber = office.RegistryPhoneNumber,
            Street = office.Street
        });

        return Unit.Value;
    }
}