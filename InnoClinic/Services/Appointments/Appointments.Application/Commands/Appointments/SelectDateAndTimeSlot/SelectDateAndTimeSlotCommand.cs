public sealed record SelectDateAndTimeSlotCommand(
    int AppointmentId,
    int ServiceId,
    int? DoctorId,
    DateTime AppointmentDate,
    TimeSpan Time) : IRequest<ErrorOr<List<TimeSlotResponse>>>
{
}
