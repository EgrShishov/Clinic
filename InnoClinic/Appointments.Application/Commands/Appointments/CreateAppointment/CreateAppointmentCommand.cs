public sealed record CreateAppointmentCommand(
    int PatientId,
    int SpecializationId,
    int DoctorId,
    int ServiceId,
    int OfficeId,
    DateTime Date,
    TimeSpan Time
    ) : IRequest<ErrorOr<Appointment>>
{
}
