public class DeleteOfficeCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteOfficeCommand, ErrorOr<Unit>>
{
    public async Task<ErrorOr<Unit>> Handle(DeleteOfficeCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync();

        try
        {
            await unitOfWork.OfficeRepository.DeleteOfficeAsync(request.Id, unitOfWork.Session);
            await unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
        return Unit.Value;
    }
}
