public class ViewAppointmentScheduleQueryHandler(
    IUnitOfWork unitOfWork, 
    IProfilesHttpClient profilesHttlClient,
    ICacheService cacheService)
    : IRequestHandler<ViewAppointmentScheduleQuery, ErrorOr<List<AppointmentsScheduleResponse>>>
{
    public async Task<ErrorOr<List<AppointmentsScheduleResponse>>> Handle(ViewAppointmentScheduleQuery request, CancellationToken cancellationToken)
    {
        List<AppointmentsScheduleResponse>? schedule = await cacheService.
            GetAsync<List<AppointmentsScheduleResponse>>($"schedule {request.AppointmentDate} {request.DoctorId}", cancellationToken);

        if (schedule is not null)
        {
            return schedule;
        }

        var appointments = await unitOfWork.AppointmentsRepository.
            ListAsync(a => a.DoctorId == request.DoctorId && a.Date == request.AppointmentDate.Date);

        if (appointments == null || !appointments.Any())
        {
            return Errors.Appointments.NotFound;
        }

        List<AppointmentsScheduleResponse?> appointmentsSchedule = new();

        foreach (var appointment in appointments)
        {
            var patient = await profilesHttlClient.GetPatientAsync(appointment.PatientId);

            if (patient == null)
            {
                return Error.NotFound();
            }

            var service = await unitOfWork.ServiceRepository.GetServiceByIdAsync(appointment.ServiceId);

            if (service == null)
            {
                return Error.NotFound();
            }

            appointmentsSchedule.Add(new AppointmentsScheduleResponse
            {
                Time = appointment.Date + appointment.Time - appointment.Time.Add(TimeSpan.FromMinutes(10)),
                PatientFullName = $"{patient.LastName} {patient.FirstName} {patient.MiddleName}",
                PatientProfileLink = $"profileservice.api/patients/{appointment.PatientId}",
                ServiceName = service.ServiceName,
                ApprovalStatus = appointment.IsApproved ? "Approved" : "Not approved",
                MedicalResultsLink = appointment.IsApproved ? $"appointments/{appointment.Id}/results" : null
            });
        }

        schedule = appointmentsSchedule.OrderBy(a => a.Time).ToList();

        await cacheService.SetAsync($"schedule {request.AppointmentDate} {request.DoctorId}", schedule, cancellationToken);

        return schedule;
    }
}
