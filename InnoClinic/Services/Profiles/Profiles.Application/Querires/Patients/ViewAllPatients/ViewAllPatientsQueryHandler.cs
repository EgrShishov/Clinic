public class ViewAllPatientsQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<ViewAllPatientsQuery, ErrorOr<List<PatientListResponse>>>
{
    public async Task<ErrorOr<List<PatientListResponse>>> Handle(ViewAllPatientsQuery request, CancellationToken cancellationToken)
    {
        var patients = await unitOfWork.PatientsRepository.GetListPatientsAsync(request.PageNumber, request.PageSize);
        if (patients is null)
        {
            return Errors.Patients.NotFound;
        }

        //var account = 
        var patientList = new List<PatientListResponse>();
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
