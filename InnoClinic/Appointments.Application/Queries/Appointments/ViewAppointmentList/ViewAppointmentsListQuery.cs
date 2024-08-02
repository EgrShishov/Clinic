public sealed record ViewAppointmentsListQuery(
    DateTime? AppointmentDate,
    int? DoctorId,
    int? ServiceId,
    bool AppointmentStatus,
    int? OfficeId) : IRequest<ErrorOr<List<Appointment>>>
{
}
