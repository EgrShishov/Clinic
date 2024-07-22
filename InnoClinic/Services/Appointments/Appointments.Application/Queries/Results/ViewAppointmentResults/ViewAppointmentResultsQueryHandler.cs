public class ViewAppointmentResultsQueryHandler(IUnitOfWork unitOfWork, IProfileService profileService, IServiceService servicesService) 
    : IRequestHandler<ViewAppointmentResultQuery, ErrorOr<ResultResponse>>
{
    public async Task<ErrorOr<ResultResponse>> Handle(ViewAppointmentResultQuery request, CancellationToken cancellationToken)
    {
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

        var patientInfo = await profileService.GetPatientAsync(appointment.PatientId);
        if (patientInfo is null)
        {
            return Error.NotFound();
        }
        string patientFullName = $"{patientInfo.LastName} {patientInfo.FirstName} {patientInfo.MiddleName}";
        
        var doctorInfo = await profileService.GetDoctorAsync(appointment.DoctorId);
        if (doctorInfo is null)
        {
            return Error.NotFound();
        }
        string doctorFullName = $"{doctorInfo.LastName} {doctorInfo.FirstName} {doctorInfo.MiddleName}";

        var serviceInfo = await servicesService.GetServiceAsync(appointment.ServiceId);
        if (serviceInfo is null)
        {
            return Error.NotFound();
        }

        return new ResultResponse(
            results.Date,
            patientFullName,
            patientInfo.DateOfBirth,
            doctorFullName,
            doctorInfo.Specialization,
            serviceInfo.ServiceName,
            results.Complaints,
            results.Conclusion,
            results.Recommendations);
    }
}
