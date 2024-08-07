public class RememberAboutUpcomingAppointmentCommandHandler(
    IUnitOfWork unitOfWork, 
    IEmailSender emailSender,
    IProfilesHttpClient profilesHttpClient,
    IAccountsHttpClient accountsHttpClient)
    : IRequestHandler<RememberAboutUpcomingAppointmentCommand, ErrorOr<Unit>>
{
    public async Task<ErrorOr<Unit>> Handle(RememberAboutUpcomingAppointmentCommand request, CancellationToken cancellationToken)
    {
        var currentDate = DateTime.UtcNow;

        var appointments = await unitOfWork.AppointmentsRepository
            .ListAsync(a => a.Date == currentDate.Date.AddDays(TimeSpan.FromDays(1).TotalMilliseconds));
        if (appointments == null)
        {
            return Errors.Appointments.NotFound;
        }

        foreach(var appointment in appointments)
        {
            var doctor = await profilesHttpClient.GetDoctorAsync(appointment.DoctorId);
            if (doctor == null)
            {
                return Error.NotFound();
            }            
            
            var patient = await profilesHttpClient.GetPatientAsync(appointment.PatientId);
            if (patient == null)
            {
                return Error.NotFound();
            }                
            
            var patientAccount = await accountsHttpClient.GetAccountInfoAsync(patient.UserId);
            if (patientAccount == null)
            {
                return Error.NotFound();
            }            
            
            var service = await unitOfWork.ServiceRepository.GetServiceByIdAsync(appointment.ServiceId);
            if (service == null)
            {
                return Error.NotFound();
            }

            string DoctorsFullName = $"{doctor.LastName} {doctor.FirstName} {doctor.MiddleName}";
            string PatientsFullName = $"{patient.LastName} {patient.FirstName} {patient.MiddleName}";

            var emailNotificationTemplate = new EmailTemplates.AppointmentNotificationEmailTemplate
            {
                AppointmentDate = appointment.Date,
                AppointmentTime = appointment.Time,
                DoctorsFullName = DoctorsFullName,
                PatientsFullName = PatientsFullName,
                ServiceName = service.ServiceName
            };

            await emailSender.SendEmailAsync(patientAccount.Email, "Upcoming appointment", emailNotificationTemplate.GetContent());
        }

        return Unit.Value;
    }
}
