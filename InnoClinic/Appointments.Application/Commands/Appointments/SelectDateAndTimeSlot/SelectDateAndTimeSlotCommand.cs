public sealed record SelectDateAndTimeSlotCommand(
    int AppointmentId,
    int ServiceId,
    DateTime Date,
    TimeSpan Time) : IRequest<ErrorOr<Appointment>>
{
}
