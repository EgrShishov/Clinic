public sealed record ViewAppointmentScheduleQuery(int DoctorId, DateTime AppointmentDate) : IRequest<ErrorOr<List<Appointment>>>
{
}
