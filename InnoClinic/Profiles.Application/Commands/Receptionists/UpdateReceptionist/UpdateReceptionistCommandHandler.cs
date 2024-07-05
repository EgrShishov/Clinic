using InnoProfileslinic.Domain.Entities;

namespace Profiles.Application.Commands.Receptionists.UpdateReceptionist
{
    public class UpdateReceptionistCommandHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<UpdateReceptionistCommand, ErrorOr<Receptionist>>
    {
        public async Task<ErrorOr<Receptionist>> Handle(UpdateReceptionistCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var receptionist = await unitOfWork.ReceptionistsRepository.GetReceptionistByIdAsync(request.ReceptionistId);

                if (receptionist is null)
                {
                    return Errors.Receptionists.NotFound;
                }

                receptionist.FirstName = request.FirstName;
                receptionist.LastName = request.LastName;
                receptionist.MiddleName = request.MiddleName;
                receptionist.OfficeId = request.OfficeId;

                await unitOfWork.ReceptionistsRepository.UpdateReceptionistAsync(receptionist);
                await unitOfWork.CompleteAsync(cancellationToken);
                await unitOfWork.CommitTransactionAsync(cancellationToken);

                return receptionist;
            }
            catch (Exception)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }
    }
}
