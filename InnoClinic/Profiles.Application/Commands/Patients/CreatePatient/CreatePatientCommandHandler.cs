public class CreatePatientCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreatePatientCommand, ErrorOr<Patient>>
{
    public async Task<ErrorOr<Patient>> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);
        //var account = await _accountRepository.GetByIdAsync(request.UserId); how can i check?
        /*if (account == null)
        {
            return Error.NotFound("Account not found", "User account does not exist.");
        }

        account.IsEmailVerified = true;
        await _accountRepository.UpdateAsync(account);

        var existingProfile = await _patientRepository.FindSimilarProfileAsync(request.FirstName, request.LastName, request.MiddleName, request.DateOfBirth);

        if (existingProfile != null && existingProfile.IsLinkedToAccount == false)
        {
            return new Error("Similar profile found", "A similar profile has been found. Do you want to link it?");
        }*/

        try
        {
            Patient patient = new Patient
            {
                AccountId = request.UserId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName,
                DateOfBirth = request.DateOfBirth,
                IsLinkedToAccount = true
            };
            var newPatient = await unitOfWork.PatientsRepository.AddPatientAsync(patient);
            await unitOfWork.CompleteAsync(cancellationToken);
            await unitOfWork.CommitTransactionAsync(cancellationToken);
            return newPatient;
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}
