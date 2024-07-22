public sealed record TimeSlot(
    DateTime StartTime,
    DateTime EndTime,
    bool IsAvailable);
