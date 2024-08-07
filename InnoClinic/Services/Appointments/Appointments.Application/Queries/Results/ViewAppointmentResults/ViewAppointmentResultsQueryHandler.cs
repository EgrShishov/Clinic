public class ViewAppointmentResultsQueryHandler(
    IUnitOfWork unitOfWork,
    IProfilesHttpClient profilesHttpClient,
    ICacheService cacheService)
    : IRequestHandler<ViewAppointmentResultQuery, ErrorOr<ResultResponse>>
{
    public async Task<ErrorOr<ResultResponse>> Handle(ViewAppointmentResultQuery request, CancellationToken cancellationToken)
    {
        ResultResponse? result = await cacheService.
            GetAsync<ResultResponse>($"results {request.ResultsId}", cancellationToken);

        if (result is not null)
        {
            return result;
        }

        var results = await unitOfWork.ResultsRepository.GetResultsByIdAsync(request.ResultsId);

        if (results is null)
        {
            return Errors.Results.NotFound;
        }

        var appointment = await unitOfWork.AppointmentsRepository.GetAppointmentByIdAsync(results.AppointmentId);

        if (appointment is null)
        {
            return Errors.Appointments.NotFound;
        }

        var patientInfo = await profilesHttpClient.GetPatientAsync(appointment.PatientId);

        if (patientInfo is null)
        {
            return Error.NotFound();
        }
        string patientFullName = $"{patientInfo.LastName} {patientInfo.FirstName} {patientInfo.MiddleName}";

        var doctorInfo = await profilesHttpClient.GetDoctorAsync(appointment.DoctorId);

        if (doctorInfo is null)
        {
            return Error.NotFound();
        }
        string doctorFullName = $"{doctorInfo.LastName} {doctorInfo.FirstName} {doctorInfo.MiddleName}";

        var serviceInfo = await unitOfWork.ServiceRepository.GetServiceByIdAsync(appointment.ServiceId);

        if (serviceInfo is null)
        {
            return Error.NotFound();
        }

        var resultResponse = new ResultResponse
        {
            AppointmentDate = results.Date,
            PatientsFullName = patientFullName,
            PatientDateOfBirth = patientInfo.DateOfBirth,
            DoctorsFullName = doctorFullName,
            DoctorsSpecialization = doctorInfo.Specialization,
            ServiceName = serviceInfo.ServiceName,
            Complaints = results.Complaints,
            Conclusion = results.Conclusion,
            Reccomendations = results.Recommendations
        };

        await cacheService.SetAsync($"results {request.ResultsId}", resultResponse, cancellationToken);

        return resultResponse;
    }
}
