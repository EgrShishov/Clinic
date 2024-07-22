
public class CreateAppointmentCommandHandler(IUnitOfWork unitOfWork, IServiceService services, IProfileService profiles) 
    : IRequestHandler<CreateAppointmentCommand, ErrorOr<Appointment>>
{
    public async Task<ErrorOr<Appointment>> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        if (!await profiles.DoctorExistsAsync(request.DoctorId))
        {
            return Error.NotFound();
        }        
        
        if (!await profiles.PatientExistsAsync(request.PatientId))
        {
            return Error.NotFound();
        }        
        
        if (!await services.ServiceExistsAsync(request.ServiceId))
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
