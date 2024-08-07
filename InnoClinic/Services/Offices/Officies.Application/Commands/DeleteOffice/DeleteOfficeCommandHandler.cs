public class DeleteOfficeCommandHandler(IUnitOfWork unitOfWork, IFilesHttpClient filesHttpClient, IEventBus eventBus) 
    : IRequestHandler<DeleteOfficeCommand, ErrorOr<Unit>>
{
    public async Task<ErrorOr<Unit>> Handle(DeleteOfficeCommand request, CancellationToken cancellationToken)
    {
        var office = await unitOfWork.OfficeRepository.GetOfficeByIdAsync(request.Id, cancellationToken);
        if (office is null)
        {
            return Errors.Offices.NotFound;
        }

        var photoDeletionResult = await filesHttpClient.DeletedPhoto($"{office.City}-{office.OfficeNumber}");
        if (photoDeletionResult.IsError)
        {
            return Error.Failure();
        }

        await unitOfWork.OfficeRepository.DeleteOfficeAsync(request.Id, cancellationToken);
        await unitOfWork.CommitTransactionAsync(cancellationToken);

        await eventBus.PublishAsync(new OfficeDeletedEvent
        {
            Id = request.Id
        });
        return Unit.Value;
    }
}
