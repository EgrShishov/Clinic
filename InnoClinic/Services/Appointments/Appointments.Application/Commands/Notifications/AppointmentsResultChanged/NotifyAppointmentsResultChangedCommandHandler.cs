public class NotifyAppointmentsResultChangedCommandHandler(
    IUnitOfWork unitOfWork,
    IEmailSender emailSender,
    IFileService fileService,
    IProfileService profileService,
    IAccountService accountService)
    : IRequestHandler<NotifyAppointmentsResultChangedCommand, ErrorOr<Unit>>
{
    public async Task<ErrorOr<Unit>> Handle(NotifyAppointmentsResultChangedCommand request, CancellationToken cancellationToken)
    {
        var appointmentResults = await unitOfWork.AppointmentsRepository.GetAppointmentByIdAsync(request.ResultsId);
        if (appointmentResults == null)
        {
            return Errors.Results.NotFound;
        }

        var document = await fileService.GetDocumentForResultAsync(request.ResultsId);
        if (document == null)
        {
            return Error.NotFound();
        }

        var profile = await profileService.GetPatientAsync(appointmentResults.PatientId);
        if (profile == null)
        {
            return Error.NotFound();
        }

        var patientAccount = await accountService.GetAccountInfoAsync(profile.UserId);
        if (patientAccount == null)
        {
            return Error.NotFound();
        }

        var mailTemplate = new NewAppointmentResultsEmailTemplate
        {
            AppointmentDate = appointmentResults.Date
        };

        await emailSender.SendEmailAsync(patientAccount.Email, "Appointments result changed", mailTemplate.GetContent(), new MemoryStream(document));

        return Unit.Value;
    }
}
