public class ViewAppointmentScheduleQueryHandler(IUnitOfWork unitOfWork, IProfileService profileService, IServiceService servicesService)
    : IRequestHandler<ViewAppointmentScheduleQuery, ErrorOr<List<AppointmentsScheduleResponse>>>
{
    public async Task<ErrorOr<List<AppointmentsScheduleResponse>>> Handle(ViewAppointmentScheduleQuery request, CancellationToken cancellationToken)
    {
        var appointments = await unitOfWork.AppointmentsRepository.
            ListAsync(a => a.DoctorId == request.DoctorId && a.Date == request.AppointmentDate.Date);
        
        if (appointments == null || !appointments.Any())
        {
            return Errors.Appointments.NotFound;
        }

        var appointmentResponses = new List<AppointmentsScheduleResponse>();

        foreach (var appointment in appointments)
        {
            var patient = await profileService.GetPatientAsync(appointment.PatientId);
            if (patient == null)
            {
                return Error.NotFound();
            }

            var service = await servicesService.GetServiceAsync(appointment.ServiceId);
            if (service == null)
            {
                return Error.NotFound();
            }

            appointmentResponses.Add(new AppointmentsScheduleResponse
            {
                Time = appointment.Date + appointment.Time - appointment.Time.Add(TimeSpan.FromMinutes(10)),
                PatientFullName = $"{patient.LastName} {patient.FirstName} {patient.MiddleName}",
                PatientProfileLink = $"profileservice.api/patients/{appointment.PatientId}",
                ServiceName = service.ServiceName,
                ApprovalStatus = appointment.IsApproved ? "Approved" : "Not approved",
                MedicalResultsLink = appointment.IsApproved ? $"appointments/{appointment.Id}/results" : null
            });
        }

        return appointmentResponses.OrderBy(a => a.Time).ToList();
    }
}
