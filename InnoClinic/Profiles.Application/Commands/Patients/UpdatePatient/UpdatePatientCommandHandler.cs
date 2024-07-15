public class UpdatePatientCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdatePatientCommand, ErrorOr<Patient>>
{
    public async Task<ErrorOr<Patient>> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var patientProfile = await unitOfWork.PatientsRepository.GetPatientByIdAsync(request.PatientId);

            if (patientProfile == null)
            {
                return Errors.Patients.NotFound;
            }

            patientProfile.FirstName = request.FirstName;
            patientProfile.LastName = request.LastName;
            patientProfile.MiddleName = request.MiddleName;
            //patientProfile.PhoneNumber = request.PhoneNumber; where i can get phone number, if it in account
            patientProfile.DateOfBirth = request.DateOfBirth;

            await unitOfWork.PatientsRepository.UpdatePatientAsync(patientProfile);
            await unitOfWork.CompleteAsync(cancellationToken);
            await unitOfWork.CommitTransactionAsync(cancellationToken);
            return patientProfile;
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}
