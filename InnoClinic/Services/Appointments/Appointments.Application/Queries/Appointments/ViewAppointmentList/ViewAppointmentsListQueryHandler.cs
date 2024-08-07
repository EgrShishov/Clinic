public class ViewAppointmentsListQueryHandler(
    IUnitOfWork unitOfWork, 
    IProfilesHttpClient profilesHttpClient,
    ICacheService cacheService) 
    : IRequestHandler<ViewAppointmentsListQuery, ErrorOr<List<AppointmentListResponse>>>
{
    public async Task<ErrorOr<List<AppointmentListResponse>>> Handle(ViewAppointmentsListQuery request, CancellationToken cancellationToken)
    {
        List<AppointmentListResponse>? appointmentsList = await cacheService.
            GetAsync<List<AppointmentListResponse>>("appointments", cancellationToken);

        if (appointmentsList is not null)
        {
            return appointmentsList;
        }

        var appointments = await unitOfWork.AppointmentsRepository.GetAllAsync(cancellationToken)
            .ContinueWith(task =>
            {
                var appointments = task.Result;
                var query = appointments.AsQueryable();

                if (request.AppointmentDate.HasValue)
                {
                    query = query.Where(a => a.Date == request.AppointmentDate);
                }

                query = query.Where(a => a.IsApproved == request.AppointmentStatus);

                if (request.DoctorId.HasValue)
                {
                    query = query.Where(a => a.DoctorId == request.DoctorId);
                }

                if (request.ServiceId.HasValue)
                {
                    query = query.Where(a => a.ServiceId == request.ServiceId);
                }

                return query.ToList();
            });

        var appointmentListResponses = new List<AppointmentListResponse>();
        foreach (var appointment in appointments)
        {
            var doctor = await profilesHttpClient.GetDoctorAsync(appointment.DoctorId);
            var patient = await profilesHttpClient.GetPatientAsync(appointment.PatientId);
            var service = await unitOfWork.ServiceRepository.GetServiceByIdAsync(appointment.ServiceId);

            if (doctor == null || patient == null || service == null)
            {
                return Error.NotFound();
            }

            string doctorFullName = $"{doctor.FirstName} {doctor.LastName} {doctor.MiddleName}";
            string patientFullName = $"{patient.FirstName} {patient.LastName} {patient.MiddleName}";

            appointmentListResponses.Add(new AppointmentListResponse
            {
                AppointmentTime = appointment.Time,
                DoctorFullName = doctorFullName,
                PatientFullName = patientFullName,
                PatientPhoneNumber = patient.PhoneNumber,
                ServiceName = service.ServiceName
            });
        }

        appointmentsList = appointmentListResponses;

        await cacheService.SetAsync("appointments", appointmentsList, cancellationToken);

        return appointmentsList;
    }
}
