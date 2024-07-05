
namespace Officies.Application.Commands.AddOffice
  public class AddOfficeCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddOfficeCommand, ErrorOr<Unit>>
{
    public async Task<ErrorOr<Unit>> Handle(AddOfficeCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync();

        try
        {
            await unitOfWork.OfficeRepository.AddOfficeAsync(request.office, unitOfWork.Session);
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
}
