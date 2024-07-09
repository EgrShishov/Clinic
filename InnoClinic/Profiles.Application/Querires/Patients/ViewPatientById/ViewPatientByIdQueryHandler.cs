public class ViewPatientByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<ViewPatientByIdQuery, ErrorOr<Patient>>
{
    public async Task<ErrorOr<Patient>> Handle(ViewPatientByIdQuery request, CancellationToken cancellationToken)
    {

        var patient = await unitOfWork.PatientsRepository.GetPatientByIdAsync(request.PatientId);

        if (patient is null)
        {
            return Errors.Patients.NotFound;
        }

        return patient;
    }
}