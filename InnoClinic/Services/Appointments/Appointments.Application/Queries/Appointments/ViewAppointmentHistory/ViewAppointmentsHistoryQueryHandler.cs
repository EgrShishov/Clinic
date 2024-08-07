public class ViewAppointmentsHistoryQueryHandler(
    IUnitOfWork unitOfWork, 
    IProfilesHttpClient profilesHttpClient,
    ICacheService cacheService)
    : IRequestHandler<ViewAppointmentsHistoryQuery, ErrorOr<List<AppointmentHistoryResponse>>>
{
    public async Task<ErrorOr<List<AppointmentHistoryResponse>>> Handle(ViewAppointmentsHistoryQuery request, CancellationToken cancellationToken)
    {
        List<AppointmentHistoryResponse>? appointmentsHistory = await cacheService.
            GetAsync<List<AppointmentHistoryResponse>>($"appointments-history {request.PatientId}");

        if (appointmentsHistory is not null)
        {
            return appointmentsHistory;
        }

        var appointments = await unitOfWork.AppointmentsRepository.ListAsync(a => a.PatientId == request.PatientId);

        if (appointments is null)
        {
            return Errors.Appointments.NotFound;
        }

        var historyResponses = new List<AppointmentHistoryResponse>();

        foreach (Appointment appointment in appointments)
        {
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

            historyResponses.Add(new AppointmentHistoryResponse
            {
                AppointmentDate = appointment.Date,
                AppointmentTime = appointment.Time,
                DoctorFullName = doctorFullName,
                ServiceName = serviceInfo.ServiceName,
                LinkToMedicalResults = appointment.IsApproved ? $"appointments/{appointment.Id}/results" : null
            });
        }

        appointmentsHistory = historyResponses;

        await cacheService.SetAsync($"appointment-history {request.PatientId}", appointmentsHistory, cancellationToken);

        return appointmentsHistory;
    }
}
