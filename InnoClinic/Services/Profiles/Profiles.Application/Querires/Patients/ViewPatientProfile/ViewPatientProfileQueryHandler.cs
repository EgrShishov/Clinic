public class ViewPatientProfileQueryHandler(IUnitOfWork unitOfWork, IAccountHttpClient accountHttpClient)
    : IRequestHandler<ViewPatientProfileQuery, ErrorOr<PatientProfileResponse>>
{
    public async Task<ErrorOr<PatientProfileResponse>> Handle(ViewPatientProfileQuery request, CancellationToken cancellationToken)
    {
        var patient = await unitOfWork.PatientsRepository.GetPatientByIdAsync(request.PatientId);

        if (patient is null)
        {
            return Errors.Patients.NotFound;
        }
        var accountResponse = await accountHttpClient.GetAccountInfo(patient.AccountId);
        if (accountResponse.IsError)
        {
            return accountResponse.FirstError;
        }

        var account = accountResponse.Value;

        return new PatientProfileResponse(
            patient.Id,
            patient.FirstName,
            patient.LastName,
            patient.MiddleName,
            patient.DateOfBirth,
            account.PhoneNumber,
            account.PhotoUrl);
    }
}