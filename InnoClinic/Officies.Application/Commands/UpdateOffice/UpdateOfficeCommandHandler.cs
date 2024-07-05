
namespace Officies.Application.Commands.UpdateOffice
{

    public class UpdateOfficeCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateOfficeCommand, ErrorOr<Unit>>
    {
        public async Task<ErrorOr<Unit>> Handle(UpdateOfficeCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.BeginTransactionAsync();

            try
            {
                await unitOfWork.OfficeRepository.UpdateOfficeAsync(request.office, unitOfWork.Session);
                await unitOfWork.CommitTransactionAsync();
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
