public sealed record SelectDateAndTimeSlotCommand(
    int AppointmentId,
    int ServiceId,
    DateTime AppointmentDate,
    TimeSpan Time) : IRequest<ErrorOr<List<TimeSlot>>>
{
}
