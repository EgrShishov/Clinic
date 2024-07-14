public class ViewPatientProfileQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<ViewPatientProfileQuery, ErrorOr<PatientProfileResponse>>
{
    public async Task<ErrorOr<PatientProfileResponse>> Handle(ViewPatientProfileQuery request, CancellationToken cancellationToken)
    {
        var patient = await unitOfWork.PatientsRepository.GetPatientByIdAsync(request.PatientId);

        if (patient is null)
        {
            return Errors.Patients.NotFound;
        }
        //var account =

        return new PatientProfileResponse(
            patient.Id,
            patient.FirstName,
            patient.LastName,
            patient.MiddleName,
            //account.PhoneNumber,
            //account.DateOfBirth,
            //account.Photo);
    }
}