using Officies.Domain.Common.Errors;

namespace Officies.Application.Commands.ChangeOfficesStatus
{
    public class ChangeOfficesStatusCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ChangeOfficesStatusCommand, ErrorOr<Unit>>
    {
        public async Task<ErrorOr<Unit>> Handle(ChangeOfficesStatusCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var office = await unitOfWork.OfficeRepository.GetOfficeByIdAsync(request.Id, unitOfWork.Session);
                if (office == null)
                {
                    return Errors.Offices.NotFound;
                }

                office.IsActive = request.isActive;

                await unitOfWork.OfficeRepository.UpdateOfficeAsync(office, unitOfWork.Session);
                await unitOfWork.CommitTransactionAsync(cancellationToken);
            }
            catch (Exception)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
            return Unit.Value;
        }
    }
}
