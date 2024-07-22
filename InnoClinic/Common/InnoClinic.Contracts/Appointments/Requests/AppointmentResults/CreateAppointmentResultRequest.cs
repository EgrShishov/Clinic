public record CreateAppointmentResultRequest(
    DateTime AppointmentDate,
    DateTime DateOfBirth,
    int PatientId,
    int DoctorId,
    int ServiceId,
    string Complaints,
    string Conclusion,
    string Recommendations);
