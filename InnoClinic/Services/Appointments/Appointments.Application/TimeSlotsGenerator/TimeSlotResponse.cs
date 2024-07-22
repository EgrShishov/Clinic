public record TimeSlotResponse(
    DateTime AppointmentDate,
    TimeSpan StartTime,
    TimeSpan EndTime,
    bool IsAvaibale);