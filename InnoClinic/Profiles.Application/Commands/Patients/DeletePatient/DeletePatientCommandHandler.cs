namespace Profiles.Application.Commands.Patients.DeletePatient
{
    public class DeletePatientCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeletePatientCommand, ErrorOr<Unit>>
    {
        public async Task<ErrorOr<Unit>> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var patient = await unitOfWork.PatientsRepository.GetPatientByIdAsync(request.PatientId);

                if(patient is null)
                {
                    return Errors.Patients.NotFound;
                }
                await unitOfWork.PatientsRepository.DeletePatientAsync(request.PatientId);
                await unitOfWork.CompleteAsync(cancellationToken);
                await unitOfWork.CommitTransactionAsync(cancellationToken);
                return Unit.Value;
            }
            catch (Exception)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }
    }
}
