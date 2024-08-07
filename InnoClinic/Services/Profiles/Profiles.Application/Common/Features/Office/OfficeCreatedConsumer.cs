public sealed class OfficeCreatedConsumer : IConsumer<OfficeChangedEvent>
{
    private readonly IUnitOfWork _unitOfWork;
    public OfficeCreatedConsumer(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Consume(ConsumeContext<OfficeChangedEvent> context)
    {
        var message = context.Message;

        var office = new Office
        {
            Id = message.Id,
            City = message.City,
            HouseNumber = message.HouseNumber,
            OfficeNumber = message.OfficeNumber,
            PhotoId = message.PhotoId,
            RegistryPhoneNumber = message.RegistryPhoneNumber,
            Street = message.Street,
        };

        await _unitOfWork.OfficeRepository.AddOfficeAsync(office);

        await _unitOfWork.CompleteAsync();
    }
}