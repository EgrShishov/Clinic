public class ViewAppointmentsHistoryQueryHandler(IUnitOfWork unitOfWork, IProfileService profileService, IServiceService servicesService)
    : IRequestHandler<ViewAppointmentsHistoryQuery, ErrorOr<List<AppointmentHistoryResponse>>>
{
    public async Task<ErrorOr<List<AppointmentHistoryResponse>>> Handle(ViewAppointmentsHistoryQuery request, CancellationToken cancellationToken)
    {
        var appointments = await unitOfWork.AppointmentsRepository.ListAsync(a => a.PatientId == request.PatientId);
        if (appointments is null)
        {
            return Errors.Appointments.NotFound;
        }


        var appointmentsHistory = new List<AppointmentHistoryResponse>();
        foreach(var appointment in appointments)
        {
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

            appointmentsHistory.Add(new AppointmentHistoryResponse
            {
                AppointmentDate = appointment.Date,
                AppointmentTime = appointment.Time,
                DoctorFullName = doctorFullName,
                ServiceName = serviceInfo.ServiceName,
                LinkToMedicalResults = appointment.IsApproved ? $"appointments/{appointment.Id}/results" : null
            });
        }

        return appointmentsHistory;
    }
}
