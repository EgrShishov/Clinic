public record AppointmentListResponse(
    TimeSpan AppointmentTime,
    string DoctorFullName,
    string PatientFullName,
    string PatientPhoneNumber,
    string ServiceName);