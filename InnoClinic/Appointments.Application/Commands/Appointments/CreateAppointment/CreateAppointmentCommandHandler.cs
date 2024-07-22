
public class CreateAppointmentCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateAppointmentCommand, ErrorOr<Appointment>>
{
    public async Task<ErrorOr<Appointment>> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
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
