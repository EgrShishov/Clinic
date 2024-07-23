public record CreateAppointmentRequest(
    int SpecializationId,
    int DoctorId,
    int ServiceId,
    int OfficeId,
    DateTime AppointmentDate,
    TimeSpan TimeSlot);