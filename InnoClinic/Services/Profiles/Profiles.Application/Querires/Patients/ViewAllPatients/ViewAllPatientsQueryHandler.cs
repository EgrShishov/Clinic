public class ViewAllPatientsQueryHandler(IUnitOfWork unitOfWork, IAccountHttpClient accountHttpClient) 
    : IRequestHandler<ViewAllPatientsQuery, ErrorOr<List<PatientListResponse>>>
{
    public async Task<ErrorOr<List<PatientListResponse>>> Handle(ViewAllPatientsQuery request, CancellationToken cancellationToken)
    {
        var patients = await unitOfWork.PatientsRepository.GetListPatientsAsync(request.PageNumber, request.PageSize);
        if (patients is null)
        {
            return Errors.Patients.NotFound;
        }

        var patientList = new List<PatientListResponse>();
        foreach (var patient in patients)
        {
            var accountResponse = await accountHttpClient.GetAccountInfo(patient.AccountId);
            if (accountResponse.IsError)
            {
                return accountResponse.FirstError;
            }

            var account = accountResponse.Value;

            patientList.Add(new PatientListResponse
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                MiddleName = patient.MiddleName,
                PhoneNumber = account.PhoneNumber
            });
        }

        return patientList;
    }
}
