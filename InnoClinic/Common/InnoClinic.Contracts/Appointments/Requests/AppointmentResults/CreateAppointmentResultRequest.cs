public record CreateAppointmentResultRequest(
    DateTime AppointmentDate,
    int PatientId,
    int DoctorId,
    int ServiceId,
    string Complaints,
    string Conclusion,
    string Recommendations);
