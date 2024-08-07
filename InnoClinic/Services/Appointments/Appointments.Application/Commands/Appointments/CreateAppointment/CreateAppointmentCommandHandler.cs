public class CreateAppointmentCommandHandler(
    IUnitOfWork unitOfWork,
    IProfilesHttpClient profilesHttpClient) : IRequestHandler<CreateAppointmentCommand, ErrorOr<Appointment>>
{
    public async Task<ErrorOr<Appointment>> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        if (!await profilesHttpClient.DoctorExistsAsync(request.DoctorId))
        {
            return Error.NotFound();
        }        
        
        if (!await profilesHttpClient.PatientExistsAsync(request.PatientId))
        {
            return Error.NotFound();
        }

        var service = await unitOfWork.ServiceRepository.GetServiceByIdAsync(request.ServiceId);
        if (service is null)
        {
            return Error.NotFound();
        }

        var appointment = new Appointment
        {
            PatientId = request.PatientId,
            IsApproved = false,
            Date = request.Date,
            Time = request.Time,
            DoctorId = request.DoctorId,
            ServiceId = request.ServiceId
        };

        var newAppointment = await unitOfWork.AppointmentsRepository.AddAppointmentAsync(appointment, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return newAppointment;
    }
}
