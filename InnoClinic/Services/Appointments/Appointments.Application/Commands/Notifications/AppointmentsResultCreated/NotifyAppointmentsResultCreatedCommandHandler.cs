public class NotifyAppointmentsResultCreatedCommandHandler(
    IUnitOfWork unitOfWork, 
    IEmailSender emailSender, 
    IFilesHttpClient filesHttpClient,
    IProfilesHttpClient profilesHttpClient,
    IAccountsHttpClient accountHttpClient)
    : IRequestHandler<NotifyAppointmentsResultCreatedCommand, ErrorOr<Unit>>
{
    public async Task<ErrorOr<Unit>> Handle(NotifyAppointmentsResultCreatedCommand request, CancellationToken cancellationToken)
    {
        var appointmentResults = await unitOfWork.AppointmentsRepository.GetAppointmentByIdAsync(request.ResultsId);
        if (appointmentResults == null)
        {
            return Errors.Results.NotFound;
        }
            
        var document = await filesHttpClient.GetDocumentForResultAsync(request.ResultsId);
        if (document == null)
        {
            return Error.NotFound();
        }        
        
        var doctor = await profilesHttpClient.GetDoctorAsync(appointmentResults.DoctorId);
        if (doctor == null)
        {
            return Error.NotFound();
        }

        var profile = await profilesHttpClient.GetPatientAsync(appointmentResults.PatientId);
        if (profile == null)
        {
            return Error.NotFound();
        }

        var patientAccount = await accountHttpClient.GetAccountInfoAsync(profile.UserId);
        if (patientAccount == null)
        {
            return Error.NotFound();
        }

        var mailTemplate = new NewAppointmentResultsEmailTemplate
        {
            AppointmentDate = appointmentResults.Date
        };
       
        await emailSender.SendEmailAsync(patientAccount.Email, "Appointments result", mailTemplate.GetContent(), new MemoryStream(document));

        return Unit.Value;
    }
}