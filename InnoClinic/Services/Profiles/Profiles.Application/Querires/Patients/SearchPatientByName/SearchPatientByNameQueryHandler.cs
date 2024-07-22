public class SearchPatientByNameQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<SearchPatientByNameQuery, ErrorOr<List<PatientListResponse>>>
{
    public async Task<ErrorOr<List<PatientListResponse>>> Handle(SearchPatientByNameQuery request, CancellationToken cancellationToken)
    {
        var patients = await unitOfWork.PatientsRepository
                    .SearchPatientByNameAsync(request.FirstName, request.LastName, request.MiddleName);
        if (patients is null)
        {
            return Errors.Patients.NotFound;
        }

        var patientList = new List<PatientListResponse>(0);
        foreach (var patient in patients)
        {
            patientList.Add(new PatientListResponse
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                MiddleName = patient.MiddleName,
                PhoneNumber = //account.PhoneNumber
            });
        }

        return patientList;
    }
}
